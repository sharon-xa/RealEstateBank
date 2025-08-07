using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RealEstateBank.Data;
using RealEstateBank.Data.Dtos.User;
using RealEstateBank.Entities;
using RealEstateBank.Interface;
using RealEstateBank.Utils;

namespace RealEstateBank.Repository;

public class UserRepository : GenericRepository<AppUser, Guid>, IUserRepository
{
    public UserRepository(
        DataContext context,
        IMapper mapper
    ) : base(context, mapper)
    {
    }

    public async Task<AppUser?> CreateUser(AppUser user)
    {
        try
        {
            return await Add(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<AppUser?> DeleteUser(Guid userId)
    {
        return await Delete(userId);
    }

    public async Task<PaginatedResult<UserDto>> GetUsers(PagingParams paging)
    {
        var users = await GetAll<UserDto>(paging: paging);
        return users;
    }

    public async Task<PaginatedResult<UserDto>> GetUsers(
        Expression<Func<AppUser, bool>> predicate,
        Func<IQueryable<AppUser>, IIncludableQueryable<AppUser, object>> include,
        PagingParams paging
    )
    {
        var users = await GetAll<UserDto>(predicate, include, paging);
        return users;
    }

    public async Task<AppUser?> UpdateUser(AppUser user)
    {
        return await Update(user);
    }
}
