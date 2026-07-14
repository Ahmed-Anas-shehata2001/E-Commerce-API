# Identity Services - Quick Reference & Usage Guide

## Authentication Service

### User Registration
```csharp
var request = new RegisterRequest
{
    Email = "user@example.com",
    Password = "SecureP@ss123",
    ConfirmPassword = "SecureP@ss123",
    FirstName = "John",
    LastName = "Doe",
    PhoneNumber = "1234567890"
};

var result = await authService.RegisterAsync(request);
if (result.Succeeded)
{
    // User created, email confirmation sent
    // User must confirm email before login
}
```

### User Login
```csharp
var request = new LoginRequest
{
    Email = "user@example.com",
    Password = "SecureP@ss123"
};

var result = await authService.LoginAsync(request);

if (result.Succeeded)
{
    // Login successful
  var accessToken = result.AccessToken;
    var refreshToken = result.RefreshToken;
 var userInfo = result.User;
}
else if (result.RequiresTwoFactor)
{
    // User has 2FA enabled, proceed to 2FA verification
}
```

### Token Refresh
```csharp
var result = await authService.RefreshTokenAsync(expiredRefreshToken);
if (result.Succeeded)
{
    var newAccessToken = result.AccessToken;
    var newRefreshToken = result.RefreshToken;
}
```

### Password Management
```csharp
// Change password (requires current password)
var changeResult = await authService.ChangePasswordAsync(new ChangePasswordRequest
{
    UserId = userId,
    CurrentPassword = "OldPassword@123",
    NewPassword = "NewPassword@456",
    ConfirmNewPassword = "NewPassword@456"
});

// Forgot password (step 1)
var forgotResult = await authService.ForgotPasswordAsync("user@example.com");
// User receives reset token via email

// Reset password (step 2)
var resetResult = await authService.ResetPasswordAsync(new ResetPasswordRequest
{
    Email = "user@example.com",
    Token = tokenFromEmail,
    NewPassword = "NewPassword@456",
    ConfirmNewPassword = "NewPassword@456"
});
```

### Two-Factor Authentication
```csharp
// Enable 2FA
var setupResult = await authService.EnableTwoFactorAsync(userId);
if (setupResult.IsSuccess)
{
    var sharedKey = setupResult.Value.SharedKey;
    var authenticatorUri = setupResult.Value.AuthenticatorUri;
 // Display QR code to user
}

// Verify 2FA code (enable)
var verifyResult = await authService.VerifyTwoFactorAsync(new TwoFactorVerifyRequest
{
    UserId = userId,
    Code = userProvidedCode // 6-digit TOTP code
});

// Login with 2FA
var loginResult = await authService.LoginWithTwoFactorAsync(new TwoFactorLoginRequest
{
    UserId = userId,
    Code = "123456"
});

// Disable 2FA
var disableResult = await authService.DisableTwoFactorAsync(userId);
```

---

## User Service

### Get User Information
```csharp
// Get by ID
var result = await userService.GetByIdAsync(userId);
if (result.IsSuccess)
{
    var userInfo = result.Value;
    var email = userInfo.Email;
    var roles = userInfo.Roles;
}

// Get by Email
var result = await userService.GetByEmailAsync("user@example.com");
```

### List All Users
```csharp
var request = new PaginationRequest
{
PageNumber = 1,
    PageSize = 20,
    SearchTerm = "john" // Optional search
};

var result = await userService.GetAllAsync(request);
if (result.IsSuccess)
{
    var pagedResult = result.Value;
    var users = pagedResult.Items;
    var totalPages = pagedResult.TotalPages;
    var hasNext = pagedResult.HasNextPage;
}
```

### Update User Profile
```csharp
var request = new UpdateUserRequest
{
 UserId = userId,
    FirstName = "Jane",
    LastName = "Smith",
    PhoneNumber = "9876543210"
};

var result = await userService.UpdateAsync(request);
```

### Account Lockout
```csharp
// Lock account for specific duration
var lockResult = await userService.LockAsync(new LockUserRequest
{
    UserId = userId,
    LockoutEndUtc = DateTimeOffset.UtcNow.AddHours(1)
});

// Lock indefinitely
var lockResult = await userService.LockAsync(new LockUserRequest
{
 UserId = userId,
    LockoutEndUtc = DateTimeOffset.MaxValue
});

// Unlock account
var unlockResult = await userService.UnlockAsync(userId);
```

### User Roles & Permissions
```csharp
// Get user's roles
var rolesResult = await userService.GetRolesAsync(userId);
if (rolesResult.IsSuccess)
{
    var roles = rolesResult.Value; // IReadOnlyList<string>
}

// Get user's permissions
var permsResult = await userService.GetPermissionsAsync(userId);
if (permsResult.IsSuccess)
{
    var permissions = permsResult.Value;
    bool canViewProducts = permissions.Contains(Permissions.Products.View);
}

// Check specific role
var isAdminResult = await userService.IsInRoleAsync(userId, "Admin");
if (isAdminResult.IsSuccess && isAdminResult.Value)
{
 // User is admin
}
```

---

## Role Service

### Manage Roles
```csharp
// Create role
var createResult = await roleService.CreateAsync(new CreateRoleRequest
{
    Name = "Manager",
    Description = "Manager role with restricted permissions"
});

// Get all roles
var allRoles = await roleService.GetAllAsync();

// Get specific role
var roleResult = await roleService.GetByIdAsync(roleId);

// Update role
var updateResult = await roleService.UpdateAsync(new UpdateRoleRequest
{
    RoleId = roleId,
    Name = "SeniorManager",
    Description = "Updated description"
});

// Delete role
var deleteResult = await roleService.DeleteAsync(roleId);
```

### Assign Roles to Users
```csharp
// Add role to user
var assignResult = await roleService.AssignRoleToUserAsync(new AssignRoleRequest
{
    UserId = userId,
    RoleName = "Manager"
});

// Remove role from user
var removeResult = await roleService.RemoveRoleFromUserAsync(new AssignRoleRequest
{
    UserId = userId,
    RoleName = "Manager"
});
```

### Manage Role Permissions
```csharp
// Assign permissions to role
var assignPermsResult = await roleService.AssignPermissionsToRoleAsync(
    new AssignPermissionsToRoleRequest
    {
  RoleId = roleId,
        PermissionNames = new[]
        {
          Permissions.Products.View,
      Permissions.Products.Create,
      Permissions.Categories.View,
         Permissions.Categories.Create
      }
    });

// Get role permissions
var permsResult = await roleService.GetRolePermissionsAsync(roleId);
if (permsResult.IsSuccess)
{
    var permissions = permsResult.Value; // IReadOnlyList<string>
}
```

---

## Current User Service

### Access Current User Information
```csharp
// In a controller or MediatR handler
public class MyController : ControllerBase
{
    private readonly ICurrentUserService _currentUserService;
    
    public MyController(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
}
    
    [Authorize]
    public IActionResult GetProfile()
    {
  if (!_currentUserService.IsAuthenticated)
       return Unauthorized();
        
        var userId = _currentUserService.UserId;
        var email = _currentUserService.Email;
        var username = _currentUserService.Username;
    var roles = _currentUserService.Roles; // IEnumerable<string>
     
        var isAdmin = _currentUserService.Roles.Contains("Admin");
        
        return Ok(new
        {
         userId,
  email,
         username,
            roles,
isAdmin
        });
    }
}
```

### Use in MediatR Behavior
```csharp
public class AuthorizationBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ICurrentUserService _currentUserService;
    
    public async Task<TResponse> Handle(
   TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_currentUserService.IsAuthenticated)
   throw new UnAuthorizedException("User is not authenticated");
    
        var userId = _currentUserService.UserId;
        // Log, audit, or perform other checks
        
     return await next();
    }
}
```

---

## Error Handling

### Check Result Success
```csharp
var result = await userService.GetByIdAsync(userId);

if (result.IsSuccess)
{
    var userInfo = result.Value;
    // Use userInfo
}
else
{
    var errorMessage = result.Error?.Message;
    var errorCode = result.Error?.Code;
    // Handle error
}
```

### Common Error Messages
- "User not found."
- "Role not found."
- "A role with this name already exists."
- "Role does not exist."
- "User is not found."
- "Invalid email or password."
- "Account is locked. Try again later."
- "Email is not confirmed yet."
- "Invalid or expired refresh token."
- "Invalid authenticator code."

---

## Best Practices

### 1. Always Check Result Status
```csharp
// ? Good
var result = await authService.LoginAsync(request);
if (result.Succeeded)
{
    // Use result.AccessToken, etc.
}
else
{
    // Handle errors: result.Errors
}

// ? Bad - Don't assume success
var token = result.AccessToken; // May be null!
```

### 2. Secure Sensitive Data
```csharp
// ? Don't log or transmit passwords
// ? Never store raw passwords

// ? Always use HTTPS for token transmission
// ? Never send tokens in query strings
```

### 3. Use Appropriate Scopes
```csharp
// ? Use .AddScoped() for per-request services
services.AddScoped<IAuthenticationService, AuthenticationService>();

// ? ICurrentUserService needs HttpContext (scoped)
services.AddScoped<ICurrentUserService, CurrentUserService>();
```

### 4. Handle Token Expiration
```csharp
// ? Catch 401 responses and use refresh token
try
{
    var response = await client.GetAsync(endpoint);
    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
 {
    var newTokens = await RefreshAccessToken();
        // Retry with new token
    }
}
catch (HttpRequestException ex)
{
    // Handle network errors
}
```

### 5. Validate Permissions Consistently
```csharp
// ? Check permissions in commands/queries
[Authorize(Policy = "AdminOnly")]
public async Task<Result> Handle(DeleteUserCommand request)
{
    // Only admins reach here
}

// ? Also check in service layer
public async Task<Result> DeleteAsync(string userId)
{
    var currentUser = _currentUserService;
    if (!currentUser.Roles.Contains("Admin"))
        return Result.Failure("Insufficient permissions");
    // Proceed with deletion
}
```

---

## Database Migrations

After adding identity to an existing project:

```bash
# Create migration
dotnet ef migrations add AddIdentity -p E-Commerce.Infrastructure

# Apply migration
dotnet ef database update -p E-Commerce.Infrastructure
```

---

## Configuration

### appsettings.json
```json
{
  "Jwt": {
    "SecretKey": "your-super-secret-key-min-32-chars",
  "Issuer": "your-app-name",
    "Audience": "your-app-audience",
    "AccessTokenExpirationMinutes": 15,
    "RefreshTokenExpirationDays": 7
  }
}
```

### Password Policy
Configured in `IdentityServiceCollectionExtensions`:
- Minimum 8 characters
- Requires uppercase letter
- Requires lowercase letter
- Requires digit
- Requires non-alphanumeric character
- Unique email per user
- Email confirmation required

---

