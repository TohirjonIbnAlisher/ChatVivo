using Enitities.Contexs;
using Enitities.EntityModels;

namespace Enitities.Repositories.MessageRepositories;

public class MessageRepository : Repository<int, Message>, IMessageRepository
{
    public MessageRepository(ChatVivoDataContex chatVivoDataContex)
        : base(chatVivoDataContex)
    {
        
    }
}
