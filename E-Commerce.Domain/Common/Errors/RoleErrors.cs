namespace E_Commerce.Domain.Common.Errors
{
    public static class RoleErrors
    {
        public static Error NotFound(Guid roleId) =>
            new("Role.NotFound", $"Role with id '{roleId}' was not found.");

        public static Error AlreadyExists(string roleName) =>
            new("Role.AlreadyExists", $"Role '{roleName}' already exists.");

        public static Error InvalidName() =>
            new("Role.InvalidName", "Role name is invalid.");

        public static Error CreateFailed(string message) =>
            new("Role.CreateFailed", message);

        public static Error UpdateFailed(string message) =>
            new("Role.UpdateFailed", message);

        public static Error DeleteFailed(string message) =>
            new("Role.DeleteFailed", message);

        public static Error CannotDeleteSystemRole(string roleName) =>
            new(
                "Role.CannotDeleteSystemRole",
                $"The system role '{roleName}' cannot be deleted.");

        public static Error InvalidPermission(string permission) =>
    new(
        "Role.InvalidPermission",
        $"Permission '{permission}' is not a valid permission.");
    }
}