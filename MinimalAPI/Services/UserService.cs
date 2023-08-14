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
        public User GetUser(UserLoginDTO loginDTO)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username.Equals(loginDTO.Username) && u.Password.Equals(loginDTO.Password));

            return user;
        }
    }
}
