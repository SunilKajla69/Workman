using BuildingBlocks.Common.Contracts.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace BuildingBlocks.Common.Infrastructure.Events
{
    /// <summary>
    /// RabbitMQ-based event publisher for microservices communication
    /// Publishes events to RabbitMQ exchange for consumption by other services
    /// </summary>
    public sealed class RabbitMqEventPublisher : IEventPublisher, IDisposable
    {
        private IConnection? _connection;
        private IChannel? _channel;
        private readonly ILogger<RabbitMqEventPublisher> _logger;
        private readonly string _exchangeName;
        private readonly bool _isConnected;

        public RabbitMqEventPublisher(IConfiguration configuration, ILogger<RabbitMqEventPublisher> logger)
        {
            _logger = logger;

            // Read RabbitMQ configuration
            var rabbitMqConfig = configuration.GetSection("RabbitMQ");
            var hostname = rabbitMqConfig["Host"] ?? "localhost";
            var port = int.Parse(rabbitMqConfig["Port"] ?? "5672");
            var username = rabbitMqConfig["Username"] ?? "guest";
            var password = rabbitMqConfig["Password"] ?? "guest";
            _exchangeName = rabbitMqConfig["ExchangeName"] ?? "workman.events";

            try
            {
                // Create RabbitMQ connection
                var factory = new ConnectionFactory
                {
                    HostName = hostname,
                    Port = port,
                    UserName = username,
                    Password = password,
                    AutomaticRecoveryEnabled = true,
                    NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
                    RequestedConnectionTimeout = TimeSpan.FromSeconds(5) // Timeout faster
                };

                _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
                _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();

                // Declare exchange (fanout for broadcasting to all subscribers)
                _channel.ExchangeDeclareAsync(
                    exchange: _exchangeName,
                    type: ExchangeType.Topic,
                    durable: true,
                    autoDelete: false).GetAwaiter().GetResult();

                _isConnected = true;

                _logger.LogInformation(
                    "? RabbitMQ EventPublisher initialized. Connected to {Host}:{Port}, Exchange: {Exchange}",
                    hostname, port, _exchangeName);
            }
            catch (Exception ex)
            {
                _isConnected = false;
                _logger.LogWarning(ex,
                    "?? Failed to initialize RabbitMQ connection. Events will NOT be published. " +
                    "Make sure RabbitMQ is running on {Host}:{Port}. " +
                    "To start RabbitMQ: docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management",
                    hostname, port);
                
                // DON'T throw - allow service to start without RabbitMQ
            }
        }

        public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) 
            where TEvent : BaseEvent
        {
            if (!_isConnected || _channel == null)
            {
                _logger.LogWarning(
                    "?? RabbitMQ is not connected. Event {EventType} with ID {EventId} will NOT be published. " +
                    "Start RabbitMQ to enable event publishing.",
                    @event.EventType,
                    @event.EventId);
                return; // Gracefully skip publishing
            }

            try
            {
                // Serialize event to JSON
                var message = JsonSerializer.Serialize(@event, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var body = Encoding.UTF8.GetBytes(message);

                // Set message properties
                var properties = new BasicProperties
                {
                    Persistent = true, // Persist messages to disk
                    ContentType = "application/json",
                    MessageId = @event.EventId.ToString(),
                    Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds()),
                    Type = @event.EventType
                };

                // Routing key is the event type (e.g., "UserRegisteredEvent")
                var routingKey = @event.EventType;

                // Publish to RabbitMQ
                await _channel.BasicPublishAsync(
                    exchange: _exchangeName,
                    routingKey: routingKey,
                    mandatory: false,
                    basicProperties: properties,
                    body: body,
                    cancellationToken: cancellationToken);

                _logger.LogInformation(
                    "? Published event {EventType} to RabbitMQ. Event ID: {EventId}, Routing Key: {RoutingKey}",
                    @event.EventType,
                    @event.EventId,
                    routingKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "? Failed to publish event {EventType} to RabbitMQ. Event ID: {EventId}",
                    @event.EventType,
                    @event.EventId);
                // Don't throw - allow operation to continue
            }
        }

        public void Dispose()
        {
            try
            {
                _channel?.CloseAsync().GetAwaiter().GetResult();
                _channel?.Dispose();
                _connection?.CloseAsync().GetAwaiter().GetResult();
                _connection?.Dispose();

                _logger.LogInformation("RabbitMQ EventPublisher disposed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error disposing RabbitMQ connection");
            }
        }
    }
}
