namespace WORKMAN.Auth.Feature.Users.DeleteUser
{
    public sealed class DeleteUserHandler
    {
        private readonly AuthDbContext _db;
        private readonly ILogger<DeleteUserHandler> _logger;

        public DeleteUserHandler(AuthDbContext db, ILogger<DeleteUserHandler> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<DeleteUserResult> Handle(
            long targetUserId,
            long currentUserId,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Soft deleting user profile. TargetUserId: {TargetUserId}, DeletedBy: {DeletedBy}",
                targetUserId,
                currentUserId);

            var user = await _db.UserProfiles
                .FirstOrDefaultAsync(
                    x => x.Id == targetUserId && x.IsDeleted == 0,
                    cancellationToken);

            if (user is null)
            {
                _logger.LogWarning(
                    "User not found or already deleted. UserId: {UserId}",
                    targetUserId);

                return new DeleteUserResult(
                    Success: false,
                    NotFound: true);
            }

            // ✅ correct audit info
            user.MarkAsDeleted(updatedBy: (int)currentUserId);

            await _db.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "User deleted successfully. UserId: {UserId}",
                targetUserId);

            return new DeleteUserResult(
                Success: true,
                NotFound: false);
        }
    }
}
