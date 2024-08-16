using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using login.Domain.Constants;
using login.Domain.Dtos.AccountDtos;
using login.Domain.Dtos.AccountDtos.Responses;
using login.Domain.Dtos.UserDtos;
using login.Domain.Entities;
using login.Domain.Services;
using login.Infrastructure.Data;
using login.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace login.Infrastructure.Services;

public class AccountService(Database database, IConfiguration config) : IAccountService
{
    private static readonly TimeSpan TokenLifeTime = TimeSpan.FromHours(8);
    public async Task<RegisterResponse> RegisterAsync(UserRegisterDto registerDto)
    {
        if (await database.Users.AnyAsync(x=> x.Login == registerDto.Login) ) throw new BadHttpRequestException("Пользователь с таким логином уже существует");
        var user = new User
        {
            Login = registerDto.Login,
            Email = registerDto.Email,
            Name = registerDto.Name,
            Role = RoleConstants.Developer,
            PasswordHashed = PasswordUtility.HashPassword(registerDto.Password)
        };

        await database.Users.AddAsync(user);

        await database.SaveChangesAsync();

        var userDto = new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name
        };

        var response = new RegisterResponse
        {
            User = userDto,
            Token = GenerateToken(user)
        };
        return response;
    }

    public async Task<LoginResponse> LoginAsync(UserLoginDto loginDto)
    {
        var user = await database.Users.FirstOrDefaultAsync(x => x.Login == loginDto.Login);
        
        if (user == null) throw new BadHttpRequestException("Логин не правильный");

        if (user.PasswordHashed != PasswordUtility.HashPassword(loginDto.Password)) throw new BadHttpRequestException("Пароль не правильный");

        var userDto = new UserDto()
        {
            Email = user.Email,
            Name = user.Name,
            Id = user.Id
        };
        var response = new LoginResponse()
        {
            Token = GenerateToken(user),
            User = userDto
        };
        return response;
    }
    private string GenerateToken(User? user)
    {
        JwtSecurityTokenHandler handler = new();
        byte[] key = Encoding.UTF8.GetBytes(config["SecuritySettings:SecurityCode"]);


        var claims = new List<Claim>()
        {
            new(ClaimTypes.Name, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role)
        };
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TokenLifeTime),
            Issuer = config["SecuritySettings:Issuer"],
            Audience = config["SecuritySettings:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        
        var token = handler.CreateToken(tokenDescriptor);

        StringBuilder stringBuilder = new StringBuilder("Bearer ");
        
        stringBuilder.Append(handler.WriteToken(token));

        return stringBuilder.ToString();
    }
   
}
