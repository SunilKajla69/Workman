using BuildingBlocks.Common.Contracts.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace BuildingBlocks.Common.Infrastructure.Events
{
    /// <summary>
    /// Background service that consumes events from RabbitMQ
    /// Runs continuously and processes incoming events
    /// </summary>
    public sealed class RabbitMqEventConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<RabbitMqEventConsumer> _logger;
        private readonly IConfiguration _configuration;
        private IConnection? _connection;
        private IChannel? _channel;
        private readonly string _queueName;
        private readonly string _exchangeName;

        public RabbitMqEventConsumer(
            IServiceProvider serviceProvider,
            ILogger<RabbitMqEventConsumer> logger,
            IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _configuration = configuration;

            // Read RabbitMQ configuration
            var rabbitMqConfig = configuration.GetSection("RabbitMQ");
            _queueName = rabbitMqConfig["QueueName"] ?? throw new InvalidOperationException("RabbitMQ QueueName not configured");
            _exchangeName = rabbitMqConfig["ExchangeName"] ?? "workman.events";
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("RabbitMQ Event Consumer starting...");

            try
            {
                await InitializeRabbitMqAsync(stoppingToken);
                await ConsumeEventsAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fatal error in RabbitMQ Event Consumer");
                throw;
            }
        }

        private async Task InitializeRabbitMqAsync(CancellationToken stoppingToken)
        {
            var rabbitMqConfig = _configuration.GetSection("RabbitMQ");
            var hostname = rabbitMqConfig["Host"] ?? "localhost";
            var port = int.Parse(rabbitMqConfig["Port"] ?? "5672");
            var username = rabbitMqConfig["Username"] ?? "guest";
            var password = rabbitMqConfig["Password"] ?? "guest";

            // Retry connection with exponential backoff
            int retryCount = 0;
            const int maxRetries = 5;

            while (!stoppingToken.IsCancellationRequested && retryCount < maxRetries)
            {
                try
                {
                    var factory = new ConnectionFactory
                    {
                        HostName = hostname,
                        Port = port,
                        UserName = username,
                        Password = password,
                        AutomaticRecoveryEnabled = true,
                        NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
                    };

                    _connection = await factory.CreateConnectionAsync();
                    _channel = await _connection.CreateChannelAsync();

                    // Declare exchange
                    await _channel.ExchangeDeclareAsync(
                        exchange: _exchangeName,
                        type: ExchangeType.Topic,
                        durable: true,
                        autoDelete: false);

                    // Declare queue
                    await _channel.QueueDeclareAsync(
                        queue: _queueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    // Bind queue to exchange with routing patterns
                    // Subscribe to all events (you can make this more specific)
                    await _channel.QueueBindAsync(
                        queue: _queueName,
                        exchange: _exchangeName,
                        routingKey: "*"); // Subscribe to all events

                    // Set QoS (process one message at a time)
                    await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

                    _logger.LogInformation(
                        "RabbitMQ Event Consumer initialized. Queue: {Queue}, Exchange: {Exchange}",
                        _queueName, _exchangeName);

                    break;
                }
                catch (Exception ex)
                {
                    retryCount++;
                    _logger.LogWarning(ex,
                        "Failed to connect to RabbitMQ (attempt {Attempt}/{MaxAttempts}). Retrying in {Delay}s...",
                        retryCount, maxRetries, Math.Pow(2, retryCount));

                    await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, retryCount)), stoppingToken);
                }
            }

            if (_connection == null || _channel == null)
            {
                throw new InvalidOperationException("Failed to initialize RabbitMQ connection after multiple retries");
            }
        }

        private async Task ConsumeEventsAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var eventType = ea.BasicProperties.Type;

                    _logger.LogInformation(
                        "Received event {EventType} from RabbitMQ. Message ID: {MessageId}",
                        eventType, ea.BasicProperties.MessageId);

                    // Process the event
                    await ProcessEventAsync(eventType, message, stoppingToken);

                    // Acknowledge the message
                    await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);

                    _logger.LogInformation(
                        "Successfully processed event {EventType}. Message ID: {MessageId}",
                        eventType, ea.BasicProperties.MessageId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        "Error processing event from RabbitMQ. Message ID: {MessageId}",
                        ea.BasicProperties.MessageId);

                    // Negative acknowledge - requeue the message
                    await _channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
                }
            };

            await _channel.BasicConsumeAsync(
                queue: _queueName,
                autoAck: false,
                consumer: consumer);

            _logger.LogInformation("RabbitMQ Event Consumer is now listening for events...");

            // Keep running until cancellation
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        private async Task ProcessEventAsync(string eventType, string messageJson, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            try
            {
                // Dynamically deserialize and invoke handler based on event type
                switch (eventType)
                {
                    case "UserRegisteredEvent":
                        var userRegisteredEvent = JsonSerializer.Deserialize<UserRegisteredEvent>(
                            messageJson,
                            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                        if (userRegisteredEvent != null)
                        {
                            var handler = scope.ServiceProvider.GetRequiredService<IEventHandler<UserRegisteredEvent>>();
                            await handler.HandleAsync(userRegisteredEvent, cancellationToken);
                        }
                        break;

                    // Add more event types here as needed
                    // case "AnotherEventType":
                    //     ...
                    //     break;

                    default:
                        _logger.LogWarning("Unknown event type: {EventType}. Skipping.", eventType);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing event {EventType}", eventType);
                throw;
            }
        }

        public override void Dispose()
        {
            try
            {
                if (_channel != null)
                {
                    _channel.CloseAsync().GetAwaiter().GetResult();
                    _channel.Dispose();
                }

                if (_connection != null)
                {
                    _connection.CloseAsync().GetAwaiter().GetResult();
                    _connection.Dispose();
                }

                _logger.LogInformation("RabbitMQ Event Consumer disposed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error disposing RabbitMQ connection");
            }

            base.Dispose();
        }
    }
}
