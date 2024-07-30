using ChatVivoService.Services;
using Enitities.EntityModels;
using Enitities.Repositories;
using Enitities.Repositories.UserRepositories;
using Microsoft.AspNetCore.Authentication;

namespace ChatVivo.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserService, UserService>();
 
        return serviceCollection;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserRepositoy, UserRepository>();

        return serviceCollection;
    }
}
