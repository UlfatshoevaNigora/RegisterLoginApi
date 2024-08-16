using login.Domain.Constants;
using login.Domain.Dtos.UserDtos;
using login.Domain.Services;
using login.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace login.Infrastructure.Services;

public class UserService(Database database) : IUserService
{
    public async Task<List<UserDto>> GetListOfUsers()
    {
        var users = await database.Users.ToListAsync();
        var userDtos = new List<UserDto>();
        foreach (var user in users)
        {
            var userDto = new UserDto()
            {
                Name = user.Name,
                Email = user.Email,
                Id = user.Id
            };
            userDtos.Add(userDto);
        }
        return userDtos;
    }
    
    public async Task<UserUpdateDto> UpdateUser(UserUpdateDto userDto)
    {
        var existing = await database.Users.FirstOrDefaultAsync(x => x.Id == userDto.Id);
        if (existing == null) throw new BadHttpRequestException("Такого пользователя нет");
        if (existing.Role != "Admin" && existing.Role!="Developer") throw new BadHttpRequestException("Такой роли нет");
        existing.Email = userDto.Email;
        existing.Name = userDto.Name;
        existing.Role = userDto.Role;
        await database.SaveChangesAsync();
        return userDto;
    }

    public async Task<bool> DeleteUser(int Id)
    {
        var user = await database.Users.FindAsync(Id);
        if (user == null) throw new BadHttpRequestException("Такого пользователя нет");

        database.Users.Remove(user);
        await database.SaveChangesAsync();
        return true;
    }
}