using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Application.Features.Admin.Sellers.Queries.GetSellers;
using E_Commerce.Application.Features.Admin.Sellers.Queries.GetSellerStatisitics;
using E_Commerce.Domain.Common.Errors;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.Catalog.ProductFeature.Entities;
using E_Commerce.Infrastructure.Identity.Autherization;
using E_Commerce.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;

namespace E_Commerce.Infrastructure.Identity.Services
{
    public sealed class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<UserService> _logger;
        private readonly ApplicationDbContext _context;

        public UserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            ILogger<UserService> logger , ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _context = context;
        }

        public async Task<Result<UserInfo>> GetUserByIdAsync(Guid userId, CancellationToken ct = default)
        {
            var user = await _userManager.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId, ct);

            if (user is null)
                return Result<UserInfo>.Failure(UserErrors.NotFound(userId));

            var info = await MapToUserInfoAsync(user);
            return Result<UserInfo>.Success(info);
        }

        public async Task<Result<UserInfo>> GetUserByEmailAsync(string email, CancellationToken ct = default)
        {
            var user = await _userManager.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.NormalizedEmail == _userManager.NormalizeEmail(email), ct);

            if (user is null)
                return Result<UserInfo>.Failure(UserErrors.NotFoundByEmail(email));

            var info = await MapToUserInfoAsync(user);
            return Result<UserInfo>.Success(info);
        }

        public async Task<Result<PagedResult<UserInfo>>> GetUsersAsync(
            PaginationRequest request, CancellationToken ct = default)
        {
            var query = _userManager.Users.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var term = request.SearchTerm.Trim();
                query = query.Where(u =>
                    EF.Functions.Like(u.Email!, $"%{term}%") ||
                    EF.Functions.Like(u.UserName!, $"%{term}%") ||
                    EF.Functions.Like(u.FirstName, $"%{term}%") ||
                    EF.Functions.Like(u.LastName, $"%{term}%"));
            }

            var totalCount = await query.CountAsync(ct);

            var users = await query
                .OrderBy(u => u.UserName)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(ct);

            var items = new List<UserInfo>(users.Count);
            foreach (var user in users)
                items.Add(await MapToUserInfoAsync(user));

            var paged = new PagedResult<UserInfo>
            {
                Items = items,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };

            return Result<PagedResult<UserInfo>>.Success(paged);
        }

        public async Task<Result> UpdateUserAsync(UpdateUserRequest request, CancellationToken ct = default)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user is null)
                return Result.Failure(UserErrors.NotFound(request.UserId));

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            return ToResult(result, errors => UserErrors.UpdateFailed(errors));
        }

        public async Task<Result> DeleteUserAsync(Guid userId, CancellationToken ct = default)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return Result.Failure(UserErrors.NotFound(userId));

            // Soft delete: safer for e-commerce (orders/audit FKs point at the user).
            user.IsDeleted = true;
            user.DeletedAtUtc = DateTimeOffset.UtcNow;
            user.LockoutEnabled = true;
            user.LockoutEnd = DateTimeOffset.MaxValue;

            var result = await _userManager.UpdateAsync(user);
            return ToResult(result, errors => UserErrors.DeleteFailed(errors));

            // Hard delete alternative:
            // var result = await _userManager.DeleteAsync(user);
            // return ToResult(result, errors => UserErrors.DeleteFailed(errors));
        }

        public async Task<Result> LockUserAsync(LockUserRequest request, CancellationToken ct = default)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user is null)
                return Result.Failure(UserErrors.NotFound(request.UserId));

            if (!user.LockoutEnabled)
            {
                var enableResult = await _userManager.SetLockoutEnabledAsync(user, true);
                if (!enableResult.Succeeded)
                    return ToResult(enableResult, errors => UserErrors.LockFailed(errors));
            }

            var lockoutEnd = request.LockoutEndUtc ?? DateTimeOffset.MaxValue;
            var result = await _userManager.SetLockoutEndDateAsync(user, lockoutEnd);
            return ToResult(result, errors => UserErrors.LockFailed(errors));
        }

        public async Task<Result> UnlockUserAsync(Guid userId, CancellationToken ct = default)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return Result.Failure(UserErrors.NotFound(userId));

            var result = await _userManager.SetLockoutEndDateAsync(user, null);
            if (!result.Succeeded)
                return ToResult(result, errors => UserErrors.UnlockFailed(errors));

            await _userManager.ResetAccessFailedCountAsync(user);
            return Result.Success();
        }

        public async Task<Result> AssignRoleToUserAsync(UserRoleRequest request, CancellationToken ct = default)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user is null)
                return Result.Failure(UserErrors.NotFound(request.UserId));

            if (!await _roleManager.RoleExistsAsync(request.RoleName))
                return Result.Failure(UserErrors.RoleNotFound(request.RoleName));

            if (await _userManager.IsInRoleAsync(user, request.RoleName))
                return Result.Failure(UserErrors.AlreadyInRole(request.RoleName));

            var result = await _userManager.AddToRoleAsync(user, request.RoleName);
            return ToResult(result, errors => UserErrors.InvalidOperation(errors));
        }

        public async Task<Result> RemoveRoleFromUserAsync(UserRoleRequest request, CancellationToken ct = default)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user is null)
                return Result.Failure(UserErrors.NotFound(request.UserId));

            if (!await _userManager.IsInRoleAsync(user, request.RoleName))
                return Result.Failure(UserErrors.NotInRole(request.RoleName));

            var result = await _userManager.RemoveFromRoleAsync(user, request.RoleName);
            return ToResult(result, errors => UserErrors.InvalidOperation(errors));
        }

        public async Task<Result<bool>> IsUserInRoleAsync(Guid userId, string roleName, CancellationToken ct = default)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return Result<bool>.Failure(UserErrors.NotFound(userId));

            var isInRole = await _userManager.IsInRoleAsync(user, roleName);
            return Result<bool>.Success(isInRole);
        }

        public async Task<Result<IReadOnlyList<string>>> GetUserRolesAsync(Guid userId, CancellationToken ct = default)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return Result<IReadOnlyList<string>>.Failure(UserErrors.NotFound(userId));

            var roles = await _userManager.GetRolesAsync(user);
            return Result<IReadOnlyList<string>>.Success(roles.ToList());
        }

        public async Task<Result> UpdateUserRolesAsync(UpdateUserRolesRequest request, CancellationToken ct = default)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user is null)
                return Result.Failure(UserErrors.NotFound(request.UserId));

            foreach (var roleName in request.RoleNames.Distinct())
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                    return Result.Failure(UserErrors.RoleNotFound(roleName));
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var desiredRoles = request.RoleNames.Distinct().ToList();

            var toRemove = currentRoles.Except(desiredRoles).ToList();
            var toAdd = desiredRoles.Except(currentRoles).ToList();

            if (toRemove.Count > 0)
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, toRemove);
                if (!removeResult.Succeeded)
                    return ToResult(removeResult, errors => UserErrors.InvalidOperation(errors));
            }

            if (toAdd.Count > 0)
            {
                var addResult = await _userManager.AddToRolesAsync(user, toAdd);
                if (!addResult.Succeeded)
                    return ToResult(addResult, errors => UserErrors.InvalidOperation(errors));
            }

            return Result.Success();
        }

        public async Task<Result<IReadOnlyList<string>>> GetUserPermissionsAsync(Guid userId, CancellationToken ct = default)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return Result<IReadOnlyList<string>>.Failure(UserErrors.NotFound(userId));

            var permissions = await GetPermissionsForUserAsync(user);
            return Result<IReadOnlyList<string>>.Success(permissions);
        }







        // seller
        public async Task<PagedResult<SellerDto>> GetSellersAsync(
    int pageNumber,
    int pageSize,
    CancellationToken ct = default)
        {
            var sellerRoleId = await _roleManager.Roles
                .Where(r => r.Name == "Seller")
                .Select(r => r.Id)
                .FirstOrDefaultAsync(ct);

            if (sellerRoleId == Guid.Empty)
            {
                return new PagedResult<SellerDto>
                {
                    Items = [],
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = 0
                };
            }

            var query =
                from user in _userManager.Users.AsNoTracking()
                join userRole in _context.UserRoles
                    on user.Id equals userRole.UserId
                where userRole.RoleId == sellerRoleId
                orderby user.UserName
                select new SellerDto
                {
                    Id = user.Id,
                    UserName = user.UserName!,
                    Email = user.Email!,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsActive = user.IsActive,
                    IsDeleted = user.IsDeleted,
                    CreatedAt = user.CreatedAt
                };

            var totalCount = await query.CountAsync(ct);

            var sellers = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return new PagedResult<SellerDto>
            {
                Items = sellers,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<Result<SellerDto>> GetSellerByIdAsync(
    Guid sellerId,
    CancellationToken ct)
        {
            var user = await _userManager.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == sellerId, ct);

            if (user is null)
                return Result<SellerDto>.Failure(
                    UserErrors.NotFound(sellerId));

            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Contains("Seller"))
                return Result<SellerDto>.Failure("User is not a seller.");

            return Result<SellerDto>.Success(new SellerDto
            {
                Id = user.Id,
                UserName = user.UserName!,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            });
        }

        public async Task<Result<SellerStatisticsDto>> GetSellerStatisticsAsync(
    Guid sellerId,
    CancellationToken ct)
        {
            var seller = await _userManager.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == sellerId, ct);

            if (seller is null)
                return Result<SellerStatisticsDto>.Failure(
                    UserErrors.NotFound(sellerId));

            var products = await _context.Products
                .Where(x => x.SellerId == sellerId)
                .ToListAsync(ct);

            var dto = new SellerStatisticsDto
            {
                SellerId = sellerId,

                ProductsCount = products.Count,

                PublishedProductsCount = products.Count(x =>
                    x.Status == ProductStatus.Published),

                ArchivedProductsCount = products.Count(x =>
                    x.Status == ProductStatus.Archived),

                TotalStock = products.Sum(x => x.Stock),

                TotalInventoryValue = products.Sum(x =>
                    x.Stock * x.Price)
            };

            return Result<SellerStatisticsDto>.Success(dto);
        }




        // ---- private helpers ----

        private async Task<UserInfo> MapToUserInfoAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var permissions = await GetPermissionsForUserAsync(user);

            return new UserInfo
            {
                Id = user.Id,
                Email = user.Email!,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                EmailConfirmed = user.EmailConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                IsLockedOut = await _userManager.IsLockedOutAsync(user),
                Roles = roles.ToList(),
                Permissions = permissions
            };
        }

        private async Task<IReadOnlyList<string>> GetPermissionsForUserAsync(ApplicationUser user)
        {
            var roleNames = await _userManager.GetRolesAsync(user);
            var permissions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var roleName in roleNames)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role is null) continue;

                var claims = await _roleManager.GetClaimsAsync(role);
                foreach (var claim in claims.Where(c => c.Type == CustomClaimTypes.Permission))
                    permissions.Add(claim.Value);
            }

            return permissions.ToList();
        }

        /// <summary>
        /// Maps a failed IdentityResult to a domain Result using the supplied Error factory,
        /// concatenating Identity's error descriptions into a single message.
        /// </summary>
        private Result ToResult(IdentityResult identityResult, Func<string, Error> errorFactory)
        {
            if (identityResult.Succeeded)
                return Result.Success();

            var errors = string.Join("; ", identityResult.Errors.Select(e => e.Description));
            _logger.LogWarning("Identity operation failed: {Errors}", errors);
            return Result.Failure(errorFactory(errors));
        }
    }
}
