using ChatVivo.Extensions;
using ChatVivoService.Hubs;
using Enitities.Contexs;
using Microsoft.EntityFrameworkCore;

namespace ChatVivo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddApplication()
                .AddRepositories();
            builder.Services.AddControllers();
            builder.Services.AddSignalR();
            builder.Services.AddDbContext<ChatVivoDataContex>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("NpgsqlConnection"));
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCors(c =>
                {
                    c.AllowAnyHeader();
                    c.AllowAnyOrigin();
                    c.SetIsOriginAllowed(c => true);
                   
                    c.AllowAnyMethod();
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapHub<ChatHub>("/chathub");
          

            app.MapControllers();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            app.Run();
        }
    }
}
