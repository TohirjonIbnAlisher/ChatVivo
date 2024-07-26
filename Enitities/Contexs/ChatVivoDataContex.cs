using Enitities.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Enitities.Contexs;

public class ChatVivoDataContex : DbContext
{
    public ChatVivoDataContex(DbContextOptions<ChatVivoDataContex> dbContextOptions)
        : base(dbContextOptions)
    {
        
    }

    public DbSet<Chat> Chats {  get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ChatMember> ChatMembers { get; set; }
    public DbSet<Message> Messages { get; set; }
}
