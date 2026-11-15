using AutoMapper;
using Bll.DTO;
using Dll.Repository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bll.Services
{
    public class MovieCommentService
    {
        private readonly MovieCommentRepository _commentRepository;
        private readonly MovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieCommentService(
            MovieCommentRepository commentRepository,
            MovieRepository movieRepository,
            IMapper mapper)
        {
            _commentRepository = commentRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<List<MovieCommentDto>> GetByMovieIdAsync(int movieId)
        {
            var comments = await _commentRepository.GetByMovieIdAsync(movieId);
            return _mapper.Map<List<MovieCommentDto>>(comments);
        }

        public async Task<MovieCommentDto?> CreateAsync(CreateMovieCommentDto dto)
        {
            if (dto == null) return null;

            var movie = await _movieRepository.GetByIdAsync($"{dto.MovieId}");
            if (movie == null) return null;

            var comment = _mapper.Map<MovieComment>(dto);

            comment.CreatedAt = DateTime.Now;

            await _commentRepository.Add(comment);
            return _mapper.Map<MovieCommentDto>(comment);
        }

        public async Task<bool> UpdateAsync(int id, UpdateMovieCommentDto dto)
        {
            if (dto == null) return false;

            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null) return false;

            comment.Text = dto.Text;

            await _commentRepository.Update(comment);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null) return false;

            await _commentRepository.Delete(comment);
            return true;
        }
    }
}
