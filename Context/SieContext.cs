using Microsoft.EntityFrameworkCore;
using SIE.Models;

namespace SIE.Context
{
    public class SIEContext : DbContext
    {
        public SIEContext(DbContextOptions<SIEContext> options) : base(options) { }

        public DbSet<Person> Person { get; set; }
        public DbSet<Institution> Institution { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<History> History { get; set; }
    }
}