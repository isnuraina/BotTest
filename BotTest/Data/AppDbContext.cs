using BotTest.Models;
using Microsoft.EntityFrameworkCore;

namespace BotTest.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<ChatHistory> ChatHistories { get; set; }
    }
}
