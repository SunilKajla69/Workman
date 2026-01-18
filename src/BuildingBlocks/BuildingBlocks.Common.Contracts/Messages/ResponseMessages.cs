namespace BuildingBlocks.Common.Contracts.Messages
{
    public static class ResponseMessages
    {
        public static class Auth
        {
            public const string LoginSuccess = "Login successful";
            public const string LoginFailed = "Invalid credentials";
            public const string LogoutSuccess = "Logout successful";
            public const string RegistrationSuccess = "Registration completed successfully";
            public const string RegistrationFailed = "Registration failed";
            public const string TokenRefreshSuccess = "Token refreshed successfully";
            public const string TokenRefreshFailed = "Invalid or expired refresh token";
            public const string UnauthorizedAccess = "Unauthorized access";
            public const string AccountLocked = "Account is locked. Please contact support";
            public const string EmailAlreadyExists = "Email address already registered";
            public const string PasswordResetSuccess = "Password reset link sent to your email";
            public const string PasswordChangeSuccess = "Password changed successfully";
        }

        public static class Validation
        {
            public const string InvalidRequest = "Invalid request data";
            public const string RequiredField = "This field is required";
            public const string InvalidEmail = "Invalid email format";
            public const string PasswordTooWeak = "Password does not meet security requirements";
            public const string InvalidToken = "Invalid or expired token";
        }

        public static class General
        {
            public const string Success = "Request completed successfully";
            public const string Failed = "Request failed";
            public const string NotFound = "Resource not found";
            public const string Created = "Resource created successfully";
            public const string Updated = "Resource updated successfully";
            public const string Deleted = "Resource deleted successfully";
            public const string UnexpectedError = "An unexpected error occurred. Please try again";
        }

        public static class UserManagement
        {
            public const string UserNotFound = "User not found";
            public const string UserCreated = "User created successfully";
            public const string UserUpdated = "User updated successfully";
            public const string UserDeleted = "User deleted successfully";
            public const string ProfileUpdated = "Profile updated successfully";
        }
    }
}
