﻿using LotoHomework.DataAccess.Context;
using LotoHomework.DataAccess.Repositories.Interfaces;
using LotoHomework.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LotoHomework.DataAccess.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly LotoDbContext _context;
        public UserRepository(LotoDbContext context)
        {
            _context = context;
        }

        public async Task DeleteByIdAsync(int id)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task InsertAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> LoginAsync(string username, string hash)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username && x.PasswordHash == hash);
        }

        public async Task UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
