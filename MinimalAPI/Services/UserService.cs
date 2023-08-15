using MinimalAPI.Data;
using MinimalAPI.Models;
using System.Reflection.Metadata.Ecma335;

namespace MinimalAPI.Services
{
    public class UserService : IUserService
    {
        private DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }
        public User? LoginUser(UserDto _userDto)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username.Equals(_userDto.Username));

            if (user != null && BCrypt.Net.BCrypt.Verify(_userDto.Password, user.PasswordHash))
                return user;
       
            return null;
        }

        public User RegisterUser(UserDto _userDto)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(_userDto.Password);

            User user = new User {Username=_userDto.Username, PasswordHash=passwordHash, Role="Standard" };
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }
    }
}
