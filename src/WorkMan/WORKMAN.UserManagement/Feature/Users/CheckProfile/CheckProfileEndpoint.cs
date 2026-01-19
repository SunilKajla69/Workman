namespace WORKMAN.UserManagement.Feature.Users.CheckProfile
{
    [ApiController]
    [Route("api/debug")]
    public sealed class CheckProfileEndpoint : ControllerBase
    {
        private readonly UserManagementDbContext _db;
        private readonly ILogger<CheckProfileEndpoint> _logger;

        public CheckProfileEndpoint(UserManagementDbContext db, ILogger<CheckProfileEndpoint> logger)
        {
            _db = db;
            _logger = logger;
        }

        /// <summary>
        /// Debug endpoint to check if a profile exists (no authorization required)
        /// </summary>
        [HttpGet("profile-exists/{id}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public async Task<ActionResult> CheckProfileExists(Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Checking if profile exists for UserId: {UserId}", id);

            var profile = await _db.UserProfiles
                .Where(u => u.Id == id)
                .Select(u => new
                {
                    u.Id,
                    u.Email,
                    u.FirstName,
                    u.LastName,
                    u.IsActive,
                    u.CreatedAt,
                    u.UpdatedAt
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (profile == null)
            {
                _logger.LogWarning("Profile NOT found for UserId: {UserId}", id);
                
                // Check total profiles in database
                var totalProfiles = await _db.UserProfiles.CountAsync(cancellationToken);
                
                return Ok(new
                {
                    exists = false,
                    userId = id,
                    message = "Profile does not exist in database",
                    totalProfilesInDb = totalProfiles,
                    troubleshooting = new
                    {
                        step1 = "Check if UserRegisteredEvent was published from Auth service",
                        step2 = "Check UserManagement logs for event processing",
                        step3 = "Verify both services are running",
                        step4 = "Register user again to trigger event"
                    }
                });
            }

            _logger.LogInformation("Profile FOUND for UserId: {UserId}", id);

            return Ok(new
            {
                exists = true,
                profile = profile,
                message = "Profile exists in database"
            });
        }

        /// <summary>
        /// Debug endpoint to list all profiles (no authorization required)
        /// </summary>
        [HttpGet("all-profiles")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllProfiles(CancellationToken cancellationToken)
        {
            var profiles = await _db.UserProfiles
                .Select(u => new
                {
                    u.Id,
                    u.Email,
                    u.FirstName,
                    u.LastName,
                    u.IsActive,
                    u.CreatedAt
                })
                .ToListAsync(cancellationToken);

            return Ok(new
            {
                totalProfiles = profiles.Count,
                profiles = profiles
            });
        }
    }
}
