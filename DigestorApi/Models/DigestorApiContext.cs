using Microsoft.EntityFrameworkCore;

namespace DigestorApi.Models
{
    public class DigestorApiContext : DbContext
    {
        public DigestorApiContext(DbContextOptions<DigestorApiContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
    }
}