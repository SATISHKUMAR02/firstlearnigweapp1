using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(UserDto dto);
        Task<bool> UpdateUserAsync(UserDto dto);
        Task<bool> DeleteUserAsync(int id);
        Task<List<UserDto>> GetUsersAsync();

        Task<UserDto> GetUserByIdAsync(int id);

        Task<UserDto> GetUserByNameAsync(string username);

        (string PasswordHash, string Salt) CreatePasswordHash(string password);

    }
}
