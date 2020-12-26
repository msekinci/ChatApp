using ChatApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Database{
    public class ChatDbContext : IdentityDbContext<User>
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options){}
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
    }
}