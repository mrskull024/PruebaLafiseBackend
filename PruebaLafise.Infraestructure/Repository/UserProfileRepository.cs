using Microsoft.EntityFrameworkCore;
using PruebaLafise.Domain.Entities;
using PruebaLafise.Domain.Interfaces;
using PruebaLafise.Infraestructure.Data;

namespace PruebaLafise.Infraestructure.Repository
{
    public class UserProfileRepository(BackendDbContext dbContext) : IUserProfileRepository
    {
        private readonly BackendDbContext _dbContext = dbContext;
        public async Task<List<UserProfile>> GetAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserProfile> GetById(int id)
        {
            return await _dbContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserProfile> Save(UserProfile user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<int> Update(UserProfile user)
        {
            return await _dbContext.Users
                .Where(model => model.Id == user.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(u => u.Name, user.Name)
                    .SetProperty(u => u.Genre, user.Genre)
                    .SetProperty(u => u.Income, user.Income)
                    .SetProperty(u => u.BirthDate, user.BirthDate));
        }
        public async Task<int> DeleteById(int id)
        {
            return await _dbContext.Users
                .Where(u => u.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}
