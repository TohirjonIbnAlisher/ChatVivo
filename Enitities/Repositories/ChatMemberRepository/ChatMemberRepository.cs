using Enitities.Contexs;
using Enitities.EntityModels;
using Enitities.Repositories.ChatRepositories;

namespace Enitities.Repositories.ChatMemberRepository;

public class ChatMemberRepository : Repository<int, ChatMember>, IChatMemberRepository
{
    public ChatMemberRepository(ChatVivoDataContex chatVivoDataContex)
        : base(chatVivoDataContex)
    {
        
    }
}
