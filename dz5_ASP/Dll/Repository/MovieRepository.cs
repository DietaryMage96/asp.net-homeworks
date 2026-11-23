using Dll.Context;
using Dll.Filters;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Dll.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _context.Movies
                .Include(m => m.Comments)
                .ToListAsync();
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _context.Movies
                .Include(m => m.Comments)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Add(Movie data)
        {
            await _context.Movies.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Movie data)
        {
            _context.Movies.Update(data);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Movie data)
        {
            _context.Movies.Remove(data);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Movie>> SearchByFilterAsync(MovieFilter filter)
        {
            var query = _context.Movies.AsQueryable();

            if (filter.Id.HasValue)
            {
                query = query.Where(m => m.Id == filter.Id.Value);
            }
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(m => m.Title.Contains(filter.Name));
            }
            if (!string.IsNullOrWhiteSpace(filter.Genre))
            {
                query = query.Where(m => m.Genre.Contains(filter.Genre));
            }
            if (filter.Year.HasValue)
            {
                query = query.Where(m => m.ReleaseYear == filter.Year.Value);
            }
            if (!string.IsNullOrWhiteSpace(filter.Descr))
            {
                query = query.Where(m => m.Description.Contains(filter.Descr));
            }
            if (!string.IsNullOrWhiteSpace(filter.Country))
            {
                query = query.Where(m => m.Country.Contains(filter.Country));
            }
            if (!string.IsNullOrWhiteSpace(filter.Key))
            {
                var key = filter.Key.Trim();

                query = query.Where(m =>
                    (m.Title != null && m.Title.Contains(key)) ||
                    (m.Description != null && m.Description.Contains(key)) ||
                    (m.Genre != null && m.Genre.Contains(key)) ||
                    m.ReleaseYear.ToString().Contains(key) ||
                    m.Id.ToString() == key
                );
            }

            return await query.ToListAsync();
        }

        // savechnages
    }    
}
