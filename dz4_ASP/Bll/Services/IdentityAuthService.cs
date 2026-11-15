using AutoMapper;
using BLL.DTO;
using Dll.Repository;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bll.Services
{
    public class IdentityAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public IdentityAuthService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<bool> CreateAsync(RegisterUserDto dto)
        {
            if (dto == null) return false;

            var existingUser = await _userManager.FindByNameAsync(dto.Login);
            if (existingUser != null) return false;

            var user = _mapper.Map<User>(dto);

            user.UserName = dto.Login;
            user.DisplayName = dto.Name ?? dto.Login;
            user.PhotoPath ??= string.Empty;

            var result = await _userManager.CreateAsync(user, dto.Pass);
            return result.Succeeded;
        }

        public async Task<UserDto?> LoginAsync(LoginUserDto request)
        {
            if (request == null) return null;

            var user = await _userManager.FindByNameAsync(request.Login);
            if (user == null) return null;

            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Pass);
            if (!isPasswordValid) return null;

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto?> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            return _mapper.Map<UserDto>(user);
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return _mapper.Map<List<UserDto>>(users);
        }
    }
}
