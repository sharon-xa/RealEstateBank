namespace RealEstateBank.Helpers;

public static class Policies {
    public const string RequireUser = "RequireUser";
    public const string RequirePublisher = "RequirePublisher";
    public const string RequireAdmin = "RequireAdmin";
    public const string RequireSuperAdmin = "RequireSuperAdmin";

    public const string RequirePublisherOrAbove = "RequirePublisherOrAbove";
    public const string RequireAdminOrAbove = "RequireAdminOrAbove";
    public const string RequireSuperAdminOnly = "RequireSuperAdminOnly";

    public const string RequirePublisherOrAdmin = "RequirePublisherOrAdmin";
    public const string RequireAuthenticated = "RequireAuthenticated";
}
