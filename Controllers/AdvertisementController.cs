using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RealEstateBank.Data.Dtos.Advertisement;
using RealEstateBank.Helpers;
using RealEstateBank.Services;
using RealEstateBank.Utils;

namespace RealEstateBank.Controllers;

[Route("api/advertisements")]
[ApiController]
public class AdvertisementController(IAdvertisementService advertisementService) : BaseController {
    private readonly IAdvertisementService _adService = advertisementService;

    [Authorize(Policy = Policies.RequirePublisherOrAbove)]
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<AdvertisementDto>>> GetAdvertisements([FromQuery] PagingParams pagingParams) {
        return await _adService.GetAll(pagingParams);
    }

    [Authorize(Policy = Policies.RequirePublisherOrAbove)]
    [HttpPost]
    public async Task<ActionResult<AdvertisementDto>> AddAdvertisement([FromBody] AdvertisementForm form) {
        return await _adService.Create(form);
    }

    [Authorize(Policy = Policies.RequirePublisherOrAbove)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAdvertisement(int id) {
        await _adService.Delete(id);
        return NoContent();
    }
}
