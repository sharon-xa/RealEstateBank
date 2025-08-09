using System.Linq.Expressions;

using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using RealEstateBank.Data;
using RealEstateBank.Entities;
using RealEstateBank.Interface;
using RealEstateBank.Utils;

namespace RealEstateBank.Repository;

public class GenericRepository<T, TId> : IGenericRepository<T, TId> where T : BaseEntity<TId> {
    protected readonly DataContext _ctx;
    protected readonly IMapper _mapper;

    protected GenericRepository(DataContext context, IMapper mapper) {
        _ctx = context;
        _mapper = mapper;
    }

    public async Task<T> Add(T entity) {
        try {
            _ctx.Set<T>().Add(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }
        catch (DbUpdateException dbEx) {
            throw new Exception("A database update error occurred while adding the entity.", dbEx);
        }
        catch (Exception ex) {
            throw new Exception("An unexpected error occurred while adding the entity.", ex);
        }
    }

    public async Task<T?> Delete(TId id) {
        var result = await GetById(id);
        if (result == null || result.Deleted)
            return null;

        _ctx.Set<T>().Remove(result);
        await _ctx.SaveChangesAsync();
        return result;
    }

    public async Task<T?> Get(Expression<Func<T, bool>> predicate) {
        var entity = await _ctx.Set<T>()
            .AsNoTracking()
            .Where(predicate)
            .FirstOrDefaultAsync();

        return entity;
    }

    public async Task<TDto?> Get<TDto>(Expression<Func<T, bool>> predicate) {
        var entity = await Get(predicate);
        return entity == null ? default : _mapper.Map<TDto>(entity);
    }

    public async Task<T?> Get(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include
    ) {
        var query = _ctx.Set<T>().AsQueryable();

        query = predicate != null ? query.Where(predicate) : query;

        if (include != null)
            query = include(query);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<PaginatedResult<T>> GetAll(
        Expression<Func<T, bool>>? predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include,
        PagingParams paging
    ) {
        var query = _ctx.Set<T>().AsQueryable();

        if (include != null)
            query = include(query);

        if (predicate != null)
            query = query.Where(predicate);

        query = query.OrderByDescending(model => model.CreationDate);

        return await PaginationHelper.CreateAsync<T>(query, paging.PageNumber, paging.PageSize);
    }

    public Task<PaginatedResult<T>> GetAll(PagingParams paging) {
        return GetAll(null, null, paging);
    }

    public Task<PaginatedResult<T>> GetAll(Expression<Func<T, bool>> predicate, PagingParams paging) {
        return GetAll(predicate, null, paging);
    }

    public Task<PaginatedResult<T>> GetAll(
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include, PagingParams paging) {
        return GetAll(null, include, paging);
    }

    public async Task<PaginatedResult<TDto>> GetAll<TDto>(
        Expression<Func<T, bool>>? predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include,
        PagingParams paging
    ) {
        var query = _ctx.Set<T>().AsQueryable();

        if (include != null)
            query = include(query);

        if (predicate != null)
            query = query.Where(predicate);

        query = query.OrderByDescending(model => model.CreationDate);

        return await PaginationHelper.CreateAsync<T, TDto>(_mapper, query, paging.PageNumber, paging.PageSize);
    }

    public Task<PaginatedResult<TDto>> GetAll<TDto>(PagingParams paging) {
        return GetAll<TDto>(null, null, paging);
    }

    public Task<PaginatedResult<TDto>> GetAll<TDto>(
        Expression<Func<T, bool>> predicate,
        PagingParams paging
    ) {
        return GetAll<TDto>(predicate, null, paging);
    }

    public Task<PaginatedResult<TDto>> GetAll<TDto>(
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
        PagingParams paging
    ) {
        return GetAll<TDto>(null, include, paging);
    }

    public async Task<T?> GetById(TId id) {
        var entity = await _ctx.Set<T>().FindAsync(id);
        if (entity == null || entity.Deleted)
            return null;

        return entity;
    }

    public async Task<T?> SoftDelete(TId id) {
        var entity = await GetById(id);
        if (entity == null || entity.Deleted)
            return null;

        entity.Deleted = true;
        _ctx.Set<T>().Update(entity);
        try {
            await _ctx.SaveChangesAsync();
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
            return null;
        }

        return entity;
    }

    public async Task<T?> Update(T entity) {
        if (entity.Deleted) return null;

        _ctx.Set<T>().Update(entity);
        try {
            await _ctx.SaveChangesAsync();
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
            return null;
        }

        return entity;
    }
}
