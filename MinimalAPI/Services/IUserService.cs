using MinimalAPI.Models;

namespace MinimalAPI.Services
{
    public interface IUserService
    {
        public User? LoginUser(UserDto _userDto);
        public User RegisterUser(UserDto _userDto);
    }
}
