using Dll.Context;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Dll.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Add(User data)
        {
            await _context.Users.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User data)
        {
            _context.Users.Update(data);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(User data)
        {
            _context.Users.Remove(data);
            await _context.SaveChangesAsync();
        }
    }
}
