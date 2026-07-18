using E_Commerce.Infrastructure.Identity;
using E_Commerce.Infrastructure.Persistence;

public class UpdateSessionActivityMiddleware
{
    private readonly RequestDelegate _next;

    public UpdateSessionActivityMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(
    HttpContext context,
    ApplicationDbContext dbContext)
    {
        await _next(context);

        if (context.User.Identity?.IsAuthenticated != true)
            return;

        var sessionIdClaim =
            context.User.FindFirst(IdentityClaimTypes.SessionId)?.Value;

        if (!Guid.TryParse(sessionIdClaim, out var sessionId))
            return;

        var session = await dbContext.UserSessions.FindAsync(
            new object[] { sessionId },
            context.RequestAborted);

        if (session is null || session.IsRevoked)
            return;

        if (DateTime.UtcNow - session.LastActivityAtUtc <= TimeSpan.FromMinutes(5))
            return;

        session.LastActivityAtUtc = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(context.RequestAborted);
    }


}
