using login.Domain.Constants;
using login.Domain.Dtos.UserDtos;
using login.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace login.Api.Controllers;

[Route("[controller]")]
[Authorize]
public class UserController(IUserService service) : Controller
{
    [HttpGet ("get-users")]
    [Authorize ]
    public async Task<ActionResult<List<UserDto>>> GetListOfUsers()
    {
        var response = await service.GetListOfUsers();
        return Ok(response);
    }
    
    [Authorize(Roles = RoleConstants.Admin)]
    [HttpPut ("update-user")]
    public async Task<ActionResult<UserUpdateDto>> UpdateUser(UserUpdateDto userDto)
    {
        var responce = await service.UpdateUser(userDto);
        return responce;
    }
    
    [HttpDelete ("delete-user")]
    public async Task<ActionResult<UserDto>> DeleteUser(int id)
    {
        var user = await service.DeleteUser(id);
        return Ok(user);
    }
}