using DatingApp.Domain.Aggregates.UserProfileAggregates;

namespace DatingApp.Application.Interfaces;

public interface IUserRepository
{
    void Update(UserProfile user);
    Task<bool> SaveAllAsync();
    Task<IEnumerable<UserProfile>> GetUsersAsync();
    Task<UserProfile?> GetUserByIdAsync(int id);
    Task<UserProfile?> GetUserByUserNameAsync(string username);
}