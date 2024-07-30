using Enitities.Contexs;
using Enitities.EntityModels;

namespace Enitities.Repositories.UserRepositories;

public class UserRepository : Repository<int, User>, IUserRepositoy
{
    public UserRepository(ChatVivoDataContex chatVivoDataContex)
        : base(chatVivoDataContex)
    {
        
    }
}
