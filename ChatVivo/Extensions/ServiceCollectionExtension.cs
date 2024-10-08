﻿using ChatVivoService.Services;
using ChatVivoService.Services.AdminServices;
using ChatVivoService.Services.ChatServices;
using ChatVivoService.Services.FileServices;
using Enitities.EntityModels;
using Enitities.Repositories;
using Enitities.Repositories.AdminRepositories;
using Enitities.Repositories.ChatRepositories;
using Enitities.Repositories.MessageRepositories;
using Enitities.Repositories.UserRepositories;
using Microsoft.AspNetCore.Authentication;

namespace ChatVivo.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserService, UserService>()
            .AddScoped<IChatService, ChatService>()
            .AddScoped<IMessageService, MessageService>()
            .AddScoped<IAdminService, AdminService>()
            .AddScoped<FileService>();
 
        return serviceCollection;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserRepositoy, UserRepository>()
            .AddScoped<IChatRepository, ChatRepository>()
            .AddScoped<IAdminRepository, AdminRepository>()
            .AddScoped<IMessageRepository, MessageRepository>();

        return serviceCollection;
    }
}
