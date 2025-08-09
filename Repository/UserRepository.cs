using System.Linq.Expressions;
using System.Net;

using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

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

    public async Task<Result<AppUser>> CreateUser(AppUser user) {
        try {
            var createdUser = await Add(user);
            return Result<AppUser>.Success(createdUser, HttpStatusCode.Created);
        }
        catch (DbUpdateException) {
            return Result<AppUser>.Fail("User already exists.", HttpStatusCode.Conflict);
        }
        catch (Exception) {
            return Result<AppUser>.Fail("Internal server error.", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<AppUser>> DeleteUser(Guid userId) {
        try {
            var deletedUser = await Delete(userId);
            if (deletedUser is null)
                return Result<AppUser>.Fail("User not found", HttpStatusCode.NotFound);

            return Result<AppUser>.Success(deletedUser);
        }
        catch (Exception ex) {
            return Result<AppUser>.Fail($"Failed to delete user: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<PaginatedResult<UserDto>>> GetUsers(PagingParams paging) {
        try {
            var users = await GetAll<UserDto>(paging);
            return Result<PaginatedResult<UserDto>>.Success(users);
        }
        catch (Exception ex) {
            return Result<PaginatedResult<UserDto>>.Fail($"Failed to get users: {ex.Message}",
                HttpStatusCode.InternalServerError);
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
            return Result<PaginatedResult<UserDto>>.Fail($"Failed to get users: {ex.Message}",
                HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<AppUser>> UpdateUser(AppUser user) {
        try {
            var updatedUser = await Update(user);
            if (updatedUser is null)
                return Result<AppUser>.Fail("User not found", HttpStatusCode.NotFound);

            return Result<AppUser>.Success(updatedUser);
        }
        catch (Exception ex) {
            return Result<AppUser>.Fail($"Failed to update user: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
}
