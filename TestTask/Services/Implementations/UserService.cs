using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUser()
        {
            return await _dbContext.Users.AsNoTracking()
                .Include(u => u.Orders)
                .OrderByDescending(u => u.Orders.Count)
                .FirstOrDefaultAsync() ?? throw new Exception("No users found");
        }

        public async Task<List<User>> GetUsers()
        {
            return await _dbContext.Users.AsNoTracking()
                .Where(u => u.Status == UserStatus.Inactive)
                .ToListAsync();
        }
    }
}