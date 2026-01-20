namespace WORKMAN.Auth.Feature.Users.SearchUsers
{
    public sealed class SearchUserHandler
    {
        private readonly AuthDbContext _db;

        public SearchUserHandler(AuthDbContext db)
        {
            _db = db;
        }

        public async Task<List<UserProfileDto>> HandleAsync(string query, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new List<UserProfileDto>();
            }

            var users = await _db.UserProfiles
                .Where(u => u.IsActive &&
                    (u.Email.Contains(query) ||
                     u.FirstName.Contains(query) ||
                     u.LastName.Contains(query)))
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
                .ToListAsync(cancellationToken);

            return users;
        }
    }
}
