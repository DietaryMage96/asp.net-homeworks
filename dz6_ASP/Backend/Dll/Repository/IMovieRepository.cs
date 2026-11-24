using Dll.Filters;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dll.Repository
{
    public interface IMovieRepository : IRepository<Movie, int>
    {
        Task<List<Movie>> SearchByFilterAsync(MovieFilter filter);
    }
}
