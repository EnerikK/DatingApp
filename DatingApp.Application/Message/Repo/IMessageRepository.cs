using DatingApp.Application.UserProfiles.Helper;
using DatingApp.Domain.Aggregates.UserProfileAggregates;

namespace DatingApp.Api.Interfaces;

public interface IMessageRepository
{
    void AddMessage(Message message);
    void DeleteMessage(Message message);
    Task<Message?> GetMessage(int id);
    Task<PagedList<MessageDto>> GetMessageForUser();
    Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername);
    Task<bool> SaveAllAsync();
}