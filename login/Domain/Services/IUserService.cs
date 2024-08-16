using login.Domain.Dtos.UserDtos;

namespace login.Domain.Services;

public interface IUserService
{
    public Task<List<UserDto>> GetListOfUsers(); 
    public Task<UserUpdateDto> UpdateUser(UserUpdateDto userDto);
    public Task<bool> DeleteUser(int Id);
}