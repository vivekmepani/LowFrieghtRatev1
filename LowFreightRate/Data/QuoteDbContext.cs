using Microsoft.EntityFrameworkCore;
using LowFreightRate.Models.PostQuote;
namespace LowFreightRate.Data
{
    public class QuoteDbContext : DbContext
    {
        public DbSet<PostQuote> Quotes { get; set; }
        public QuoteDbContext(DbContextOptions<QuoteDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
