using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using RealEstateBank.Utils;

namespace RealEstateBank.Interface;

public interface IGenericRepository<T, TId>
{
    Task<T?> GetById(TId id);
    Task<PaginatedResult<T>> GetAll(PagingParams paging);
    Task<PaginatedResult<T>> GetAll(
        Expression<Func<T, bool>> predicate,
        PagingParams paging
    );
    Task<PaginatedResult<T>> GetAll(
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
        PagingParams paging
    );
    Task<PaginatedResult<T>> GetAll(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
        PagingParams paging
    );
    Task<PaginatedResult<TDto>> GetAll<TDto>(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
        PagingParams paging
    );
    Task<PaginatedResult<TDto>> GetAll<TDto>(PagingParams paging);
    Task<PaginatedResult<TDto>> GetAll<TDto>(
        Expression<Func<T, bool>> predicate,
        PagingParams paging
    );
    Task<PaginatedResult<TDto>> GetAll<TDto>(
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
        PagingParams paging
    );

    Task<T> Add(T entity);
    Task<T?> Delete(TId id);
    Task<T?> Update(T entity);
    Task<T?> SoftDelete(TId id);
    Task<TDto?> Get<TDto>(Expression<Func<T, bool>> predicate);
    Task<T?> Get(Expression<Func<T, bool>> predicate);
    Task<T?> Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include);
}