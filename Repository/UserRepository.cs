using System.Linq.Expressions;
using System.Net;

using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using Npgsql;

using RealEstateBank.Data;
using RealEstateBank.Data.Dtos.User;
using RealEstateBank.Entities;
using RealEstateBank.Interface;
using RealEstateBank.Utils;

namespace RealEstateBank.Repository;

public class UserRepository : GenericRepository<AppUser, Guid>, IUserRepository {
    public UserRepository(
        DataContext context,
        IMapper mapper
    ) : base(context, mapper) {
    }

    public async Task<AppUser> CreateUser(AppUser user) {
        try {
            return await Add(user);
        }
        catch (DbUpdateException ex) {
            throw;
        }
    }

    public async Task<Result<AppUser>> DeleteUser(Guid userId) {
        try {
            var deletedUser = await Delete(userId);
            if (deletedUser == null)
                return Result<AppUser>.ExpectedFail("User not found", HttpStatusCode.NotFound);
            return Result<AppUser>.Success(deletedUser);
        }
        catch (PostgresException pgEx) {
            return Result<AppUser>.DbFail("Failed to delete user", pgEx.SqlState, pgEx.Message);
        }
        catch (Exception ex) {
            return Result<AppUser>.Fail("Failed to delete user", ex.Message);
        }
    }

    public async Task<Result<PaginatedResult<UserDto>>> GetUsers(
        Expression<Func<AppUser, bool>> predicate,
        Func<IQueryable<AppUser>, IIncludableQueryable<AppUser, object>> include,
        PagingParams paging) {
        try {
            var users = await GetAll<UserDto>(predicate, include, paging);
            return Result<PaginatedResult<UserDto>>.Success(users);
        }
        catch (Exception ex) {
            return Result<PaginatedResult<UserDto>>.Fail($"Failed to get users", ex.Message);
        }
    }

    public async Task<Result<AppUser>> UpdateUser(AppUser user) {
        try {
            var updatedUser = await Update(user);
            if (updatedUser is null)
                return Result<AppUser>.ExpectedFail("User not found", HttpStatusCode.NotFound);

            return Result<AppUser>.Success(updatedUser);
        }
        catch (Exception ex) {
            return Result<AppUser>.Fail("Failed to update user", ex.Message);
        }
    }
}
