using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

namespace RealEstateBank.Utils;

public class PaginatedResult<T> {
    public PaginationMetadata Metadata { get; set; } = null!;
    public List<T> Items { get; set; } = [];
}

public class PaginationHelper {
    public static async Task<PaginatedResult<T>> CreateAsync<T>(IQueryable<T> query, int pageNumber, int pageSize) {
        var count = await query.CountAsync();
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedResult<T> {
            Metadata = new PaginationMetadata {
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                PageSize = pageSize,
                TotalCount = count
            },
            Items = items
        };
    }

    public static async Task<PaginatedResult<TDto>> CreateAsync<T, TDto>(
        IMapper mapper,
        IQueryable<T> query,
        int pageNumber,
        int pageSize
    ) {
        var count = await query.CountAsync();
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectTo<TDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return new PaginatedResult<TDto> {
            Metadata = new PaginationMetadata {
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                PageSize = pageSize,
                TotalCount = count
            },
            Items = items
        };
    }
}

public class PaginationMetadata {
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
}
