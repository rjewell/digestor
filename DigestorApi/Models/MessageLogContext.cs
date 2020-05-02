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
    }
}