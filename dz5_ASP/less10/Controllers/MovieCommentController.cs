using AutoMapper;
using Bll.DTO;
using Bll.Services;
using FluentValidation;
using less10.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace less10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieCommentController : ControllerBase
    {
        private readonly MovieCommentService _commentService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateMovieCommentRequest> _createValidator;
        private readonly IValidator<UpdateMovieCommentRequest> _updateValidator;

        public MovieCommentController(
            MovieCommentService commentService,
            IMapper mapper,
            IValidator<CreateMovieCommentRequest> createValidator,
            IValidator<UpdateMovieCommentRequest> updateValidator)
        {
            _commentService = commentService;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet("movie/{movieId:int}")]
        public async Task<ActionResult<List<MovieCommentResponse>>> GetByMovieId(int movieId)
        {
            var dtos = await _commentService.GetByMovieIdAsync(movieId);
            return Ok(_mapper.Map<List<MovieCommentResponse>>(dtos));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMovieCommentRequest request)
        {
            var validationResult = await _createValidator.ValidateAsync(request);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var dto = _mapper.Map<CreateMovieCommentDto>(request);
            var result = await _commentService.CreateAsync(dto);

            if (result == null) return BadRequest("Movie does not exist");

            return Ok(_mapper.Map<MovieCommentResponse>(result));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMovieCommentRequest request)
        {
            var validationResult = await _updateValidator.ValidateAsync(request);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var dto = _mapper.Map<UpdateMovieCommentDto>(request);
            bool isUpdated = await _commentService.UpdateAsync(id, dto);

            if (!isUpdated) return NotFound("Comment not found");

            return Ok("Comment updated");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await _commentService.DeleteAsync(id);
            if (!isDeleted) return NotFound("Comment not found");

            return Ok("Comment deleted");
        }
    }
}
