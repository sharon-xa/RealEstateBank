using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using RealEstateBank.Data.Enums;

namespace RealEstateBank.Extensions;

public static class ClaimsPrincipalExtensions {
    private static string? GetClaimValue(this ClaimsPrincipal user, string claimName) {
        var claims = (user.Identity as ClaimsIdentity)?.Claims;
        if (claims == null) return null;

        var claim = claims.FirstOrDefault(c =>
            string.Equals(c.Type, claimName, StringComparison.CurrentCultureIgnoreCase) &&
            !string.Equals(c.Type, "null", StringComparison.CurrentCultureIgnoreCase)
        );
        if (claim == null) return null;

        return claim.Value.Replace("\"", "");
    }

    public static Guid? GetUserId(this ClaimsPrincipal user) {
        var idStr = user.GetClaimValue(JwtRegisteredClaimNames.Sub);
        var found = Guid.TryParse(idStr, out var id);

        if (!found) return null;

        return id;
    }

    public static UserRole? GetUserRole(this ClaimsPrincipal user) {
        var roleInClaims = user.GetClaimValue(ClaimTypes.Role);
        var parsed = Enum.TryParse<UserRole>(roleInClaims, out var role);
        if (!parsed) return null;
        return role;
    }

    public static DateTime? GetTokenExpiry(this ClaimsPrincipal user) {
        var expiryDate = user.GetClaimValue(JwtRegisteredClaimNames.Exp);
        var parsed = DateTime.TryParse(
            expiryDate,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out var expiry
        );
        if (!parsed) return null;

        return expiry;
    }
}
