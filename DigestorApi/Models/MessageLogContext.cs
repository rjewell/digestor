using Microsoft.EntityFrameworkCore;

namespace DigestorApi.Models
{
    public class MessageLogContext : DbContext
    {
        public MessageLogContext(DbContextOptions<MessageLogContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("Messages");
            modelBuilder.Entity<Message>().ToContainer("Messages");
        }
    }
}