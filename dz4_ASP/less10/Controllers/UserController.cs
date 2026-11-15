//    using AutoMapper;
//    using Bll.Services;
//    using BLL.DTO;
//    using Domain.Models;
//    using FluentValidation;
//    using less10.Contracts;
//    using less10.DTOs;
//    using Microsoft.AspNetCore.Mvc;

//    namespace less10.Controllers
//    {
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController(
//    UserService _userService,
//    IValidator<RegisterRequest> _registerValidator,
//    IValidator<Contracts.LoginRequest> _loginValidator,
//    IMapper _mapper) : ControllerBase
//    {
//        [HttpGet]
//        public async Task<ActionResult<List<UserResponse>>> GetAll()
//        {
//            var dtos = await _userService.GetAllAsync();
//            var response = _mapper.Map<List<UserResponse>>(dtos);
//            return Ok(response);
//        }

//        [HttpPost("register")]
//        public async Task<IActionResult> Register([FromBody] RegisterRequest data)
//        {
//            var validationResult = await _registerValidator.ValidateAsync(data);
//            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

//            var dto = _mapper.Map<RegisterUserDto>(data);

//            bool isCreated = await _userService.CreateAsync(dto);
//            if (!isCreated) return BadRequest("User already exists or creation failed");

//            return Ok("User created successfully");
//        }

//        [HttpGet("{id:int}")]
//        public async Task<ActionResult<UserResponse>> GetById(string id)
//        {
//            var dto = await _userService.GetByIdAsync(id);
//            if (dto == null) return NotFound("User not found");

//            var response = _mapper.Map<UserResponse>(dto);
//            return Ok(response);
//        }

//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] LoginRequest data)
//        {
//            var validationResult = await _loginValidator.ValidateAsync(data);
//            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

//            var dto = _mapper.Map<LoginUserDto>(data);

//            var userDto = await _userService.LoginAsync(dto);
//            if (userDto == null) return Unauthorized("Invalid login or pass");

//            var response = _mapper.Map<UserResponse>(userDto);
//            return Ok(response);
//        }
//    }
//}


// Identity

using AutoMapper;
using Bll.Services;
using BLL.DTO;
using FluentValidation;
using less10.Contracts;
using less10.DTOs;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace less10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(
        IdentityAuthService _authService,
        IValidator<IdentityRegisterRequest> _registerValidator,
        IValidator<Contracts.LoginRequest> _loginValidator,
        IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<UserResponse>>> GetAll()
        {
            var dtos = await _authService.GetAllAsync();
            var response = _mapper.Map<List<UserResponse>>(dtos);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] IdentityRegisterRequest data)
        {
            var validationResult = await _registerValidator.ValidateAsync(data);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var dto = _mapper.Map<RegisterUserDto>(data);

            bool isCreated = await _authService.CreateAsync(dto);
            if (!isCreated) return BadRequest("User already exists or creation failed");

            return Ok("User created successfully");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetById(string id)
        {
            var dto = await _authService.GetByIdAsync(id);
            if (dto == null) return NotFound("User not found");

            var response = _mapper.Map<UserResponse>(dto);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Contracts.LoginRequest data)
        {
            var validationResult = await _loginValidator.ValidateAsync(data);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var dto = _mapper.Map<LoginUserDto>(data);

            var userDto = await _authService.LoginAsync(dto);
            if (userDto == null) return Unauthorized("Invalid login or pass");

            var response = _mapper.Map<UserResponse>(userDto);
            return Ok(response);
        }
    }
}