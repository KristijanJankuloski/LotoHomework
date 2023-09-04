using LotoHomework.DataAccess.Context;
using LotoHomework.DataAccess.Repositories.Interfaces;
using LotoHomework.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LotoHomework.DataAccess.Repositories.Implementations
{
    public class SessionRepository : ISessionRepository
    {
        private readonly LotoDbContext _context;
        public SessionRepository(LotoDbContext context)
        {
            _context = context;
        }

        public async Task DeleteByIdAsync(int id)
        {
            Session session = await _context.Sessions.FirstOrDefaultAsync(s => s.Id == id);
            if (session != null)
            {
                return;
            }
            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Session>> GetAllAsync()
        {
            return await _context.Sessions.ToListAsync();
        }

        public async Task<Session> GetByIdAsync(int id)
        {
            return await _context.Sessions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Session> GetLatestActiveAsync()
        {
            return await _context.Sessions.Include(x => x.EntryCombinations).LastOrDefaultAsync(x => !x.IsEnded);
        }

        public async Task InsertAsync(Session entity)
        {
            await _context.Sessions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Session entity)
        {
            _context.Sessions.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
