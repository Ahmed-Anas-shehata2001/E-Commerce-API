namespace E_Commerce.Application.Common.Contracts.Identity.Models
{

    public class RoleDto
    {
        public Guid Id { get; init; } = default!;
        public string Name { get; init; } = default!;
        public string? Description { get; init; }
        public IReadOnlyList<string> Permissions { get; init; } = Array.Empty<string>();
    }

    public class CreateRoleRequest
    {
        public string Name { get; init; } = default!;
        public string? Description { get; init; }
    }

    public class UpdateRoleRequest
    {
        public Guid RoleId { get; init; } = default!;
        public string Name { get; init; } = default!;
        public string? Description { get; init; }
    }


    public class UpdateRolePermissionsRequest
    {
        public Guid RoleId { get; init; } = default!;
        public IReadOnlyList<string> PermissionNames { get; init; } = Array.Empty<string>();
    }
}

