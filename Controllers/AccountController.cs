using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RealEstateBank.Data.Dtos.User;
using RealEstateBank.Data.Enums;
using RealEstateBank.Extensions;
using RealEstateBank.Helpers;
using RealEstateBank.Services;

namespace RealEstateBank.Controllers;

[Route("account")]
public class AccountController(IUserService userService) : BaseController {
    private readonly IUserService _userService = userService;

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register([FromBody] RegisterForm form) {
        return await _userService.Register(form);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginForm loginForm) {
        return await _userService.Login(loginForm);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> GetUser() {
        var userId = User.GetUserId();
        if (userId == null)
            return Unauthorized("User ID claim is missing or invalid");

        var user = await _userService.GetUserById(userId.Value);
        if (user == null)
            return NotFound("user not found");

        return user;
    }

    [Authorize(Policy = Policies.RequireSuperAdminOnly)]
    [HttpPatch("promote/{userId}/admin")]
    public async Task<IActionResult> PromoteToAdmin(Guid userId) {
        var promoted = await _userService.UpdateUserRole(userId, UserRole.Admin);
        if (promoted == null)
            return BadRequest("No such user");

        if (promoted == false)
            return StatusCode(500, "Couldn't promote user");

        return Ok();
    }
}
