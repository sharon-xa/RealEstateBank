using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealEstateBank.Data.Dtos.User;
using RealEstateBank.Services;

namespace RealEstateBank.Controllers;

[Route("account")]
public class AccountController(IUserService userService) : BaseController
{
    private readonly IUserService _userService = userService;

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<UserDto>> Register([FromBody] RegisterForm form)
    {
        return await _userService.Register(form);
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginForm loginForm)
    {
        return await _userService.Login(loginForm);
    }
}