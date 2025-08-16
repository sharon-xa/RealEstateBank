using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RealEstateBank.Data.Dtos.Bank;
using RealEstateBank.Helpers;
using RealEstateBank.Services;

namespace RealEstateBank.Controllers;

[Route("api/bank")]
[ApiController]
public class BankController(IBankService bankService) : BaseController {
    private readonly IBankService _bankService = bankService;

    [Authorize(Policy = Policies.RequirePublisherOrAbove)]
    [HttpGet("")]
    public async Task<ActionResult<BankDto>> GetBank() {
        return await _bankService.GetBank();
    }

    [Authorize(Policy = Policies.RequireAdminOrAbove)]
    [HttpPatch("about-us")]
    public async Task<ActionResult<BankDto>> UpdateAboutUs([FromBody] string aboutUs) {
        return await _bankService.UpdateBankAboutUs(aboutUs);
    }

    [Authorize(Policy = Policies.RequireAdminOrAbove)]
    [HttpPatch("about-real-estate-bank")]
    public async Task<ActionResult<BankDto>> UpdateAboutRealEstateBank([FromBody] RealEstateBankForm form) {
        return await _bankService.UpdateAboutRealEstateBank(form);
    }

    // TODO: upload video
    [Authorize(Policy = Policies.RequireAdminOrAbove)]
    [HttpPatch("electronic-payment-dep")]
    public async Task<ActionResult<BankDto>> UpdateElectronicPaymentDep([FromBody] ElectronicPaymentDepForm form) {
        return await _bankService.UpdateElectronicPaymentDep(form);
    }
}
