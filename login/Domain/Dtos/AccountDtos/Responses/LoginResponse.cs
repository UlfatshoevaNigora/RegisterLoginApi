using login.Domain.Dtos.UserDtos;

namespace login.Domain.Dtos.AccountDtos.Responses;

public class LoginResponse
{
    public UserDto? User { get; set; }
    public string? Token { get; set; }
}