using AutoMapper;
using ePizza.Application.Contracts;
using ePizza.Application.DTOs.Request;
using ePizza.Domain.Entities;
using ePizza.Domain.Interfaces;

namespace ePizza.Application.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserTokenRepository _userTokenRepository;

        public UserService(
            IUserRepository userRepository, 
            IMapper mapper,
            IUserTokenRepository userTokenRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userTokenRepository = userTokenRepository;
        }

        public async Task<UserDomain> GetUserAsync(string username)
        {
            var user =  await _userRepository.FindAsync(x => x.Email.Equals(username));
            return user.FirstOrDefault()!;
        }

        public async Task<UserTokenDomain> GetUserTokenAsync(int userId)
        {
            return await _userTokenRepository.GetUserTokenAsync(userId);
        }

        public async Task<bool> RegisterUserAsync(RegisterUserDto register)
        {
            // after validation

            var userDomain = _mapper.Map<UserDomain>(register);

            userDomain.Password = BCrypt.Net.BCrypt.HashPassword(register.Password);

            userDomain.UserRoles = new List<string> { RolesEnum.User.ToString() };

            int rowsInserted = await _userRepository.AddUserAsync(userDomain);

            return rowsInserted > 0;
        }
    }
}
