using DatingApp.Application.UserProfiles.Helper;
using DatingApp.DataAccess;
using DatingApp.Domain.Aggregates.UserProfileAggregates;

namespace DatingApp.Api.Interfaces;

public class MessageRepository(DataContext dataContext) : IMessageRepository
{
    public void AddMessage(Message message)
    {
        dataContext.Messages.Add(message);
    }

    public void DeleteMessage(Message message)
    {
        dataContext.Messages.Remove(message);
    }

    public async Task<Message?> GetMessage(int id)
    {
        return await dataContext.Messages.FindAsync(id);
    }

    public Task<PagedList<MessageDto>> GetMessageForUser()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await dataContext.SaveChangesAsync() > 0;
    }
}