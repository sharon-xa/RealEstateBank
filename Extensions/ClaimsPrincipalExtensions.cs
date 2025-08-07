using System.Globalization;
using System.Security.Claims;
using RealEstateBank.Data.Enums;
using RealEstateBank.Helpers;

namespace RealEstateBank.Extensions;

public static class ClaimsPrincipalExtensions
{
    private static string GetClaimValue(this ClaimsPrincipal user, string claimName)
    {
        var claims = (user.Identity as ClaimsIdentity)?.Claims;
        if (claims == null)
            throw new Exception("Claims not found");

        var claim = claims.FirstOrDefault(c =>
            string.Equals(c.Type, claimName, StringComparison.CurrentCultureIgnoreCase) &&
            !string.Equals(c.Type, "null", StringComparison.CurrentCultureIgnoreCase)
        );

        if (claim == null)
            throw new Exception($"Claim ({claimName}) not found");

        return claim.Value.Replace("\"", "");
    }

    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var idStr = user.GetClaimValue(ClaimNames.Id);
        var found = Guid.TryParse(idStr, out var id);

        if (!found)
            throw new Exception($"Claim (Id) not found");

        return id;
    }

    public static UserRole GetUserRole(this ClaimsPrincipal user)
    {
        var roleInClaims = user.GetClaimValue(ClaimNames.Role);
        var parsed = Enum.TryParse<UserRole>(roleInClaims, out var role);
        if (!parsed)
            throw new Exception($"Claim (Role) not found");
        return role;
    }

    public static DateTime GetTokenExpiry(this ClaimsPrincipal user)
    {
        var expiryDate = user.GetClaimValue(ClaimNames.ExpiryDate);
        var parsed = DateTime.TryParse(
            expiryDate,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out var expiry
        );
        if (!parsed)
            throw new Exception($"Claim (ExpiryDate) not found");
        return expiry;
    }

    public static Guid? GetParentId(this ClaimsPrincipal user)
    {
        var parentIdStr = user.GetClaimValue(ClaimNames.ParentId);

        if (
            !string.IsNullOrWhiteSpace(parentIdStr) &&
            !string.Equals(parentIdStr, "null", StringComparison.OrdinalIgnoreCase)
        )
        {
            return Guid.TryParse(parentIdStr, out var parentId) ? parentId : null;
        }

        return null;
    }
}