using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore.Query;

using RealEstateBank.Data.Dtos.User;
using RealEstateBank.Entities;
using RealEstateBank.Utils;

namespace RealEstateBank.Interface;

public interface IUserRepository : IGenericRepository<AppUser, Guid> {
    Task<Result<AppUser>> CreateUser(AppUser user);
    Task<Result<PaginatedResult<UserDto>>> GetUsers(PagingParams paging);

    Task<Result<PaginatedResult<UserDto>>> GetUsers(
        Expression<Func<AppUser, bool>> predicate,
        Func<IQueryable<AppUser>, IIncludableQueryable<AppUser, object>> include,
        PagingParams paging
    );

    Task<Result<AppUser>> UpdateUser(AppUser user);
    Task<Result<AppUser>> DeleteUser(Guid userId);
}
