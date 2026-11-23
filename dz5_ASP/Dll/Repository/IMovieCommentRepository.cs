using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dll.Repository
{
    public interface IMovieCommentRepository : IRepository<MovieComment, int>
    {
        Task<List<MovieComment>> GetByMovieIdAsync(int movieId);
    }
}
