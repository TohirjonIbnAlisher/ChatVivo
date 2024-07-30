using Enitities.Contexs;
using Enitities.EntityModels;

namespace Enitities.Repositories.ChatRepositories;

public class ChatRepository : Repository<int, Chat>, IChatRepository
{
    public ChatRepository(ChatVivoDataContex chatVivoDataContex)
        : base(chatVivoDataContex)
    {
        
    }
}
