using AutoMapper;

using ClosedXML.Excel;

using Microsoft.EntityFrameworkCore;

using RealEstateBank.Data;
using RealEstateBank.Data.Dtos.AcceptedCitizen;
using RealEstateBank.Entities;
using RealEstateBank.Interface;
using RealEstateBank.Utils;
using RealEstateBank.Utils.Exceptions;

namespace RealEstateBank.Services;

public interface IAcceptedCitizenService {
    Task<PaginatedResult<AcceptedCitizenDto>> GetAll(PagingParams pagingParams);
    Task<List<AcceptedCitizenDto>> ImportAcceptedCitizens(IFormFile file);
}

public class AcceptedCitizenService : IAcceptedCitizenService {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public AcceptedCitizenService(
        IRepositoryWrapper repositoryWrapper,
        IMapper mapper,
        DataContext context
    ) {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _context = context;
    }

    public async Task<List<AcceptedCitizenDto>> ImportAcceptedCitizens(IFormFile file) {
        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        stream.Position = 0;

        using var workbook = new XLWorkbook(stream);
        var worksheet = workbook.Worksheet(1);
        if (worksheet == null)
            throw new AppException(
                "no worksheet found in this excel file",
                nameof(AcceptedCitizenService),
                nameof(ImportAcceptedCitizens),
                StatusCodes.Status400BadRequest
            );

        var rows = worksheet.RangeUsed().RowsUsed();
        var citizens = new List<AcceptedCitizen>();
        var branches = await _context.Branches.ToListAsync();
        var branchLookup = branches.ToLookup(b => b.City);

        foreach (var row in rows.Skip(1)) {
            string fullName = row.Cell(1).GetValue<string>().Trim();
            string stateStr = row.Cell(2).GetValue<string>().Trim();
            string branchName = row.Cell(3).GetValue<string>().Trim();

            bool acceptanceState = stateStr.Equals("Accepted", StringComparison.OrdinalIgnoreCase);

            if (!branchLookup.Contains(branchName))
                continue;

            var branch = branchLookup[branchName].First();

            citizens.Add(new AcceptedCitizen {
                FullName = fullName,
                AcceptanceState = acceptanceState,
                BranchId = branch.Id,
                Branch = branch
            });
        }
        await _context.AcceptedCitizens.AddRangeAsync(citizens);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<List<AcceptedCitizenDto>>(citizens);
        return result;
    }

    public async Task<PaginatedResult<AcceptedCitizenDto>> GetAll(PagingParams pagingParams) {
        return await _repositoryWrapper.AcceptedCitizens.GetAll<AcceptedCitizenDto>(
            query => query.Include(c => c.Branch),
            pagingParams
        );
    }
}
