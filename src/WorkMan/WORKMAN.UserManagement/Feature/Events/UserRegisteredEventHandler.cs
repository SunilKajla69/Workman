namespace WORKMAN.UserManagement.Feature.Events
{
    public sealed class UserRegisteredEventHandler : IEventHandler<UserRegisteredEvent>
    {
        private readonly UserManagementDbContext _db;
        private readonly ILogger<UserRegisteredEventHandler> _logger;

        public UserRegisteredEventHandler(UserManagementDbContext db, ILogger<UserRegisteredEventHandler> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task HandleAsync(UserRegisteredEvent @event, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Processing UserRegistered event for UserId: {UserId}, Email: {Email}",
            @event.UserId, @event.Email);

            try
            {
                // Check if profile already exists (idempotency)
                var existingProfile = await _db.UserProfiles
                    .FirstOrDefaultAsync(p => p.Id == @event.UserId, cancellationToken);

                if (existingProfile is not null)
                {
                    _logger.LogWarning("Profile already exists for UserId: {UserId}", @event.UserId);
                    return;
                }

                // Create new profile with minimal information
                var profile = new UserProfile(@event.UserId, @event.Email, "", "");
                _db.UserProfiles.Add(profile);

                await _db.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Created user profile for UserId: {UserId}", @event.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process UserRegistered event for UserId: {UserId}", @event.UserId);
                throw; // Re-throw to let event publisher handle retry logic
            }
        }
    }
}
