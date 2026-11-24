using AutoMapper;
using Bll.DTO;
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
        private readonly SignInManager<User> _signInManager;
        private readonly JwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;
        public IdentityAuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper,
            JwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
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

        public async Task<LoginResponceDTO> LoginAsync(LoginUserDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Login);
            if (user == null)
            {
                return null;
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Pass, true);
            if (!result.Succeeded)
            {
                return null;
            }
            return _jwtTokenService.CreateToken(user);
        }

        public async Task<UserDto?> LoginAsyncOld(LoginUserDto request)
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

        public async Task<bool> ValidateUserCredentialsAsync(LoginUserDto loginDto)
        {
            if (loginDto == null) return false;

            var user = await _userManager.FindByNameAsync(loginDto.Login);
            if (user == null) return false;

            //return await _userManager.CheckPasswordAsync(user, loginDto.Pass);
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Pass, false);
            return result.Succeeded;
        }
    }
}
