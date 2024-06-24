using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TaskTracker.Database.Models;

namespace TaskTracker.Database
{
    public partial class TaskTrackerContext : DbContext
    {
        public TaskTrackerContext()
        { }

        public TaskTrackerContext(DbContextOptions<TaskTrackerContext> options)
            : base(options)
        { }


        public void EnsureMigrationIsApplied(bool isDevelopmentEnvironment)
        {
            #if DEBUG
                var data = Database.GetDbConnection();
                Database.EnsureDeleted();
                Database.Migrate();
                //AddMockData();
            #else
                    Database.Migrate();
            #endif
        }

        public virtual DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost,1433;Database=TaskTracker;TrustServerCertificate=true;Trusted_Connection=false;User Id=sa;Password=dock3rP@ssword");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //OnModelCreatingPartial(modelBuilder);
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}