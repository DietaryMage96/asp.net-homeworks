using AutoMapper;
using BLL.DTO;
using Dll.Filters;
using Dll.Repository;
using Domain.Models;
using static System.Net.WebRequestMethods;

namespace Bll.Services
{
    public class MovieService
    {
        private readonly MovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieService(MovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(CreateMovieDto dto)
        {
            if (dto == null) return false;

            var movies = await _movieRepository.GetAllAsync();
            if (movies.Any(m => m.Title == dto.Title)) return false;

            var movie = _mapper.Map<Movie>(dto);

            await _movieRepository.Add(movie);
            return true;
        }

        public async Task<MovieDto?> GetByIdAsync(string id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null) return null;

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<List<MovieDto>> GetAllAsync()
        {
            var movies = await _movieRepository.GetAllAsync();
            return _mapper.Map<List<MovieDto>>(movies);
        }

        public async Task<bool> UpdateAsync(string id, UpdateMovieDto dto)
        {
            if (dto == null) return false;

            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null) return false;

            _mapper.Map(dto, movie);

            await _movieRepository.Update(movie);
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null) return false;

            await _movieRepository.Delete(movie);
            return true;
        }

        public async Task<List<MovieDto>> SearchMovieByFilterAsync(MovieFilter filter)
        {
            var movies = await _movieRepository.SearchByFilterAsync(filter);
            return _mapper.Map<List<MovieDto>>(movies);
        }
    }
}
