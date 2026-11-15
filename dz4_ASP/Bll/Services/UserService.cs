using AutoMapper;
using BLL.DTO;
using Dll.Repository;
using Domain.Models;

namespace Bll.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(UserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(RegisterUserDto dto)
        {
            if (dto == null) return false;

            var users = await _userRepository.GetAllAsync();
            if (users.Any(u => u.UserName == dto.Login)) return false;

            var user = _mapper.Map<User>(dto);


            user.UserName = dto.Login;
            user.NormalizedUserName = dto.Login;
            user.PasswordHash = dto.Pass;
            user.DisplayName = dto.Name ?? dto.Login;
            user.PhotoPath ??= string.Empty;

            await _userRepository.Add(user);
            return true;
        }

        public async Task<UserDto?> LoginAsync(LoginUserDto request)
        {
            if (request == null) return null;

            var users = await _userRepository.GetAllAsync();
            var user = users.FirstOrDefault(u => u.UserName == request.Login && u.PasswordHash == request.Pass);

            if (user == null) return null;

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto?> GetByIdAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            return _mapper.Map<UserDto>(user);
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<List<UserDto>>(users);
        }
    }
}