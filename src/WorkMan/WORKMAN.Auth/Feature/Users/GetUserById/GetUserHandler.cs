namespace WORKMAN.Auth.Feature.Users.GetUser
{
    public sealed class GetUserHandler
    {
        private readonly AuthDbContext _db;

        public GetUserHandler(AuthDbContext db)
        {
            _db = db;
        }

        public async Task<UserProfileDto?> HandleAsync(long userId, CancellationToken cancellationToken)
        {
            Guard.AgainstNull(userId, nameof(userId));

            var user = await _db.UserProfiles
                .Where(u => u.Id == userId && u.IsActive)
                .Select(u => new UserProfileDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt
                })
                .FirstOrDefaultAsync(cancellationToken);

            return user;
        }
    }
}
