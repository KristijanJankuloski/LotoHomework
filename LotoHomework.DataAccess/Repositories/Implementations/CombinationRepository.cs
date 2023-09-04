using LotoHomework.DataAccess.Context;
using LotoHomework.DataAccess.Repositories.Interfaces;
using LotoHomework.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LotoHomework.DataAccess.Repositories.Implementations
{
    public class CombinationRepository : ICombinationRepository
    {
        private readonly LotoDbContext _context;
        public CombinationRepository(LotoDbContext context)
        {
            _context = context;
        }

        public async Task DeleteByIdAsync(int id)
        {
            Combination combination = await _context.Combinations.FirstOrDefaultAsync(c => c.Id == id);
            if (combination == null)
            {
                return;
            }
            _context.Combinations.Remove(combination);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Combination>> GetAllAsync()
        {
            return await _context.Combinations.ToListAsync();
        }

        public async Task<Combination> GetByIdAsync(int id)
        {
            return await _context.Combinations.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(Combination entity)
        {
            await _context.Combinations.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Combination entity)
        {
            _context.Combinations.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
