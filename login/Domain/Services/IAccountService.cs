using login.Domain.Dtos.AccountDtos;
using login.Domain.Dtos.AccountDtos.Responses;

namespace login.Domain.Services;

public interface IAccountService
{
    public Task<RegisterResponse> RegisterAsync(UserRegisterDto registerDto);
    public Task<LoginResponse> LoginAsync(UserLoginDto loginDto);
}