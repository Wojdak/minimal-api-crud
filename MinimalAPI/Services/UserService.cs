using AutoMapper;
using MinimalAPI.Data;
using MinimalAPI.Models;
using System.Reflection.Metadata.Ecma335;

namespace MinimalAPI.Services
{
    public class UserService : IUserService
    {
        private DataContext _context;
        private readonly IMapper _mapper;

        public UserService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            var user = _mapper.Map<User>(_userDto); 

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }
    }
}
