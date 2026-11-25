using Dll.Context;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dll.Repository
{
    public class MovieCommentRepository : IMovieCommentRepository
    {
        private readonly AppDbContext _context;

        public MovieCommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovieComment>> GetAllAsync()
        {
            return await _context.MovieComments.ToListAsync();
        }

        public async Task<MovieComment?> GetByIdAsync(int id)
        {
            return await _context.MovieComments.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<MovieComment>> GetByMovieIdAsync(int movieId)
        {
            return await _context.MovieComments
                .Where(c => c.MovieId == movieId)
                .ToListAsync();
        }

        public async Task Add(MovieComment comment)
        {
            await _context.MovieComments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task Update(MovieComment comment)
        {
            _context.MovieComments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(MovieComment comment)
        {
            _context.MovieComments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}
