using RealEstateBank.Data.Enums;
using RealEstateBank.Utils;

namespace RealEstateBank.Data.Dtos.User;

public class UserFilter : PagingParams
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public UserRole? Role { get; set; }
}
