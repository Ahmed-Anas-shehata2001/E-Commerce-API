namespace E_Commerce.Domain.Common.Errors
{
    public static class UserErrors
    {
        public static Error NotFound(Guid userId) =>
            new(
                "User.NotFound",
                $"User with id '{userId}' was not found.");

        public static Error NotFoundByEmail(string email) =>
    new(
        "User.NotFound",
        $"User with email '{email}' was not found.");
        public static Error EmailAlreadyExists(string email) =>
            new(
                "User.EmailAlreadyExists",
                $"A user with email '{email}' already exists.");

        public static Error UserNameAlreadyExists(string userName) =>
            new(
                "User.UserNameAlreadyExists",
                $"A user with username '{userName}' already exists.");

        public static Error InvalidEmail() =>
            new(
                "User.InvalidEmail",
                "The email address is invalid.");

        public static Error InvalidUserName() =>
            new(
                "User.InvalidUserName",
                "The username is invalid.");

        public static Error UpdateFailed(string message) =>
            new(
                "User.UpdateFailed",
                message);

        public static Error DeleteFailed(string message) =>
            new(
                "User.DeleteFailed",
                message);

        public static Error LockFailed(string message) =>
            new(
                "User.LockFailed",
                message);

        public static Error UnlockFailed(string message) =>
            new(
                "User.UnlockFailed",
                message);

        public static Error AlreadyInRole(string roleName) =>
            new(
                "User.AlreadyInRole",
                $"The user is already assigned to role '{roleName}'.");

        public static Error NotInRole(string roleName) =>
            new(
                "User.NotInRole",
                $"The user is not assigned to role '{roleName}'.");

        public static Error RoleNotFound(string roleName) =>
            new(
                "User.RoleNotFound",
                $"Role '{roleName}' was not found.");

        public static Error CannotRemoveLastAdmin() =>
            new(
                "User.CannotRemoveLastAdmin",
                "The last administrator cannot be removed from the Admin role.");

        public static Error InvalidOperation(string message) =>
            new(
                "User.InvalidOperation",
                message);
    }
}