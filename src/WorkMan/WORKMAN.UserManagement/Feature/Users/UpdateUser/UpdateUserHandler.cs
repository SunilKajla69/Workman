using WORKMAN.UserManagement.Feature.Users.GetUser;

namespace WORKMAN.UserManagement.Feature.Users.UpdateUser
{
    /// <summary>
    /// Handler for updating user profile information
    /// Follows Command pattern - modifies state without returning domain entities
    /// Returns DTO for API response
    /// </summary>
    public sealed class UpdateUserHandler
    {
        private readonly UserManagementDbContext _db;
        private readonly ILogger<UpdateUserHandler> _logger;

        public UpdateUserHandler(UserManagementDbContext db, ILogger<UpdateUserHandler> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<UserProfileDto> HandleAsync(
            long userId, 
            UpdateUserRequest request, 
            CancellationToken cancellationToken)
        {
            Guard.AgainstNull(userId, nameof(userId));
            Guard.AgainstNull(request, nameof(request));

            _logger.LogInformation("Updating profile for UserId: {UserId}", userId);

            // Retrieve the user profile
            var profile = await _db.UserProfiles
                .FirstOrDefaultAsync(p => p.Id == userId, cancellationToken)
                ?? throw new InvalidOperationException($"User profile not found for UserId: {userId}");

            // Check if user is active
            if (!profile.IsActive)
            {
                _logger.LogWarning("Attempted to update inactive profile for UserId: {UserId}", userId);
                throw new InvalidOperationException("Cannot update inactive user profile");
            }

            // Domain logic encapsulated in entity method
            // Validates and updates - throws if validation fails
            profile.Update(
                request.FirstName,
                request.LastName,
                request.PhoneNumber);

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

            // Return DTO (not domain entity)
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
