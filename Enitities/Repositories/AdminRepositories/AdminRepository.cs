using Enitities.Contexs;
using Enitities.EntityModels;

namespace Enitities.Repositories.AdminRepositories;

public class AdminRepository : Repository<int, Admin>, IAdminRepository
{
    public AdminRepository(ChatVivoDataContex chatVivoDataContex)
        : base(chatVivoDataContex)
    {
    }
}
