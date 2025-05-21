using PruebaLafise.Domain.Entities;

namespace PruebaLafise.Domain.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<List<UserProfile>> GetAll();
        Task<UserProfile> GetById(int id);
        Task<UserProfile> Save(UserProfile user);
        Task<int> Update(UserProfile user);
        Task<int> DeleteById(int id);
    }
}
