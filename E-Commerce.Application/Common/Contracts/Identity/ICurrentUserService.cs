
namespace E_Commerce.Application.Common.Contracts.Identity
{
    public interface ICurrentUserService
    {
        ///This service should never access the database.

        //info ( claims ) about the current authenticated user   -->  (usually via HttpContext.User).*/

        // It answers only who's making the request.

        Guid? UserId { get; }
        string? Email { get; }
        string? Username { get; }
        bool IsAuthenticated { get; }
        IReadOnlyCollection<string> Roles { get; }
        bool IsInRole(string role);
        bool HasPermission(string permission);



    }
}
