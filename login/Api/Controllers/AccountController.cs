
using login.Domain.Dtos.AccountDtos;
using login.Domain.Dtos.AccountDtos.Responses;
using login.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace login.Api.Controllers;

[Route("[controller]")]
public class AccountController(IAccountService service) : Controller
{
    [HttpPost("Register")]
    public async Task<ActionResult<RegisterResponse>> Register([FromBody] UserRegisterDto registerDto)
    {
        var response = await service.RegisterAsync(registerDto);
        return Ok(response);
    }
    
    [HttpPost("Login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] UserLoginDto loginDto)
    {
        var response = await service.LoginAsync(loginDto);
        return Ok(response);
    }
}