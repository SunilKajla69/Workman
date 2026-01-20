namespace WORKMAN.Auth.Feature.Users.GetUsers
{
    public class GetUsersHandler
    {
        private readonly AuthDbContext _db;

        public GetUsersHandler(AuthDbContext db)
        {
            _db = db;
        }
        public async Task<List<UserProfileDto>> HandleAsync(CancellationToken cancellationToken)
        {
            // Only return users that are not deleted
            return await _db.UserProfiles
                .Where(u => u.IsDeleted == 0)
                .Select(u => new UserProfileDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    IsDeleted = u.IsDeleted,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}
    