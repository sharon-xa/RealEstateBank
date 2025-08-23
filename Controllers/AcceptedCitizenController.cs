using Microsoft.AspNetCore.Mvc;

using RealEstateBank.Data.Dtos.AcceptedCitizen;
using RealEstateBank.Helpers;
using RealEstateBank.Services;
using RealEstateBank.Utils;

namespace RealEstateBank.Controllers;

[Route("api/accepted-citizens")]
[ApiController]
public class AcceptedCitizenController(IAcceptedCitizenService acceptedCitizen) : ControllerBase {
    private readonly IAcceptedCitizenService _acceptedCitizenService = acceptedCitizen;

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<AcceptedCitizenDto>>> GetAll([FromQuery] PagingParams pagingParams) {
        return await _acceptedCitizenService.GetAll(pagingParams);
    }

    [HttpPost("document")]
    public async Task<IActionResult> AddAcceptedCitizens(IFormFile file) {
        const int MaxFileSize = 5 * 1024 * 1024;

        var err = SafeFileUpload.CheckFile(file, ["xlsx"], MaxFileSize);
        if (err != null)
            return BadRequest(err);

        return Ok(await _acceptedCitizenService.ImportAcceptedCitizens(file));
    }
}