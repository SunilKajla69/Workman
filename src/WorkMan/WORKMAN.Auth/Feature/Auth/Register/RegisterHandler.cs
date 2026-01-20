using BuildingBlocks.Common.Contracts.Events;

namespace WORKMAN.Auth.Feature.Auth.Register
{
    public sealed class RegisterHandler
    {
        private readonly AuthDbContext _authDb;
        private readonly PasswordHasher _hasher;
        private readonly IEventPublisher _eventPublisher;

        public RegisterHandler(AuthDbContext authDb, PasswordHasher hasher, IEventPublisher eventPublisher)
        {
            _authDb = authDb;
            _hasher = hasher;
            _eventPublisher = eventPublisher;
        }

        public async Task<RegisterResponse> HandleAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            var email = NormalizeEmail(request.Email);
            
            var passwordHash = _hasher.Hash(request.Password);

            var user = new User(email, passwordHash);

            _authDb.Users.Add(user);

            try
            {
                await _authDb.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException)
            {
                throw new InvalidOperationException("Email already registered.");
            }

            // Create minimal profile - user can complete it later via Update Profile endpoint
            var userProfile = new UserProfile(
                userId: user.Id,
                email: user.Email
            );

            _authDb.UserProfiles.Add(userProfile);
            await _authDb.SaveChangesAsync(cancellationToken);

            //Publish Event: Notify other services about new user registration

            var userRegisteredEvent = new UserRegisteredEvent
            {
                UserId = user.Id,
                Email = user.Email,
                RegisteredAt = user.CreatedAt
            };

            await _eventPublisher.PublishAsync(userRegisteredEvent, cancellationToken);

            return new RegisterResponse
            {
                UserId = user.Id,
                Email = user.Email
            };
        }
        private static string NormalizeEmail(string email)
        {
            return email.Trim().ToLowerInvariant();
        }
    }
}
