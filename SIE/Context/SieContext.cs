using Microsoft.EntityFrameworkCore;

namespace SIE.Context
{
    public class SIEContext : DbContext
    {
        public SIEContext(DbContextOptions<SIEContext> options) : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Institution> Institution { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<RelStudentRoom> RelStudentRoom { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<PasswordRecovery> PasswordRecovery { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<RelUploadActivity> RelUploadActivity { get; set; }
    }
}