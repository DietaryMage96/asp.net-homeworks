using AutoMapper;
using Bll.Services;
using BLL.DTO;
using Dll.Filters;
using FluentValidation;
using less10.Contracts;
using less10.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace less10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController(
        MovieService _movieService,
        IValidator<CreateMovieRequest> _createValidator,
        IValidator<UpdateMovieRequest> _updateValidator,
        IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<MovieResponse>>> GetAll()
        {
            var dtos = await _movieService.GetAllAsync();
            var response = _mapper.Map<List<MovieResponse>>(dtos);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieResponse>> GetById(string id)
        {
            var dto = await _movieService.GetByIdAsync(id);
            if (dto == null) return NotFound("Movie not found");

            var response = _mapper.Map<MovieResponse>(dto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMovieRequest data)
        {
            var validationResult = await _createValidator.ValidateAsync(data);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var dto = _mapper.Map<CreateMovieDto>(data);

            bool isCreated = await _movieService.CreateAsync(dto);
            if (!isCreated) return BadRequest("Movie already exists");

            return Ok("Movie created");
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateMovieRequest data)
        {
            var validationResult = await _updateValidator.ValidateAsync(data);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var dto = _mapper.Map<UpdateMovieDto>(data);

            bool isUpdated = await _movieService.UpdateAsync(id, dto);
            if (!isUpdated) return NotFound("Movie not found");

            return Ok("Movie updated");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool isDeleted = await _movieService.DeleteAsync(id);
            if (!isDeleted) return NotFound("Movie not found");

            return Ok("Movie deleted");
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<MovieResponse>>> Search([FromQuery] MovieFilter filter)
        {
            var dtos = await _movieService.SearchMovieByFilterAsync(filter);
            var response = _mapper.Map<List<MovieResponse>>(dtos);
            return Ok(response);
        }
    }
}
