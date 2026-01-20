namespace WORKMAN.Auth.Feature.Users.UpdateUser
{
    public sealed class UpdateUserHandler
    {
        private readonly AuthDbContext _db;
        private readonly ILogger<UpdateUserHandler> _logger;

        public UpdateUserHandler(AuthDbContext db, ILogger<UpdateUserHandler> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<UserProfileDto> HandleAsync(
            long userId, 
            UpdateUserRequest request, 
            CancellationToken cancellationToken)
        {
            if (userId <= 0)
                throw new ArgumentException("UserId must be greater than zero", nameof(userId));
            
            Guard.AgainstNull(request, nameof(request));

            _logger.LogInformation("Updating profile for UserId: {UserId}", userId);

            var profile = await _db.UserProfiles
                .FirstOrDefaultAsync(p => p.Id == userId, cancellationToken)
                ?? throw new InvalidOperationException($"User profile not found for UserId: {userId}");

            if (!profile.IsActive)
            {
                _logger.LogWarning("Attempted to update inactive profile for UserId: {UserId}", userId);
                throw new InvalidOperationException("Cannot update inactive user profile");
            }

            profile.Update(
                request.FirstName,
                request.LastName,
                request.PhoneNumber,
                (int)userId); // Track who updated the profile

            // Persist changes
            try
            {
                await _db.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Successfully updated profile for UserId: {UserId}", userId);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency conflict updating profile for UserId: {UserId}", userId);
                throw new InvalidOperationException("Profile was modified by another process. Please retry.", ex);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error updating profile for UserId: {UserId}", userId);
                throw new InvalidOperationException("Failed to update profile. Please try again.", ex);
            }

            return new UserProfileDto
            {
                Id = profile.Id,
                Email = profile.Email,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                PhoneNumber = profile.PhoneNumber,
                CreatedAt = profile.CreatedAt,
                UpdatedAt = profile.UpdatedAt
            };
        }
    }
}
