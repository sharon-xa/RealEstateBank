using Microsoft.AspNetCore.Mvc;

using RealEstateBank.Data.Dtos.AcceptedCitizen;
using RealEstateBank.Helpers;
using RealEstateBank.Services;
using RealEstateBank.Utils;

namespace RealEstateBank.Controllers;

[Route("api/accepted-citizens")]
[ApiController]
public class AcceptedCitizenController(IAcceptedCitizenService acceptedCitizen) : ControllerBase {
    private readonly IAcceptedCitizenService _acceptedCitizen = acceptedCitizen;

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<AcceptedCitizenDto>>> GetAll([FromQuery] PagingParams pagingParams) {
        return await _acceptedCitizen.GetAll(pagingParams);
    }

    [HttpPost]
    public async Task<IActionResult> AddAcceptedCitizens(IFormFile file) {
        const int MaxFileSize5MB = 5 * 1024 * 1024;

        var err = SafeFileUpload.CheckFile(file, ["xlsx"], MaxFileSize5MB);
        if (err != null)
            return BadRequest(err);

        return Ok();
    }
}