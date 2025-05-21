using PruebaLafise.Domain.Entities;
using PruebaLafise.Domain.Interfaces;

namespace PruebaLafise.Application.Services
{
    public class UserProfileService(IUserProfileRepository repository) : IUserProfileService
    {
        private readonly IUserProfileRepository _repository = repository;

        public Task<UserProfile> GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Task<List<UserProfile>> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<UserProfile> Save(UserProfile user)
        {
            return _repository.Save(user);
        }

        public Task<int> Update(UserProfile user)
        {
            return _repository.Update(user);
        }

        public Task<int> DeleteById(int id)
        {
            return _repository.DeleteById(id);
        }
    }
}
