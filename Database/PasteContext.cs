using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class PasteContext : DbContext
    {
        public DbSet<Paste> Pastes { get; set; }
        public DbSet<ViewCount> ViewCounts { get; set; }

        public PasteContext(DbContextOptions<PasteContext> options)
            : base(options)
        {
        }
    }
}