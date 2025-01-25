using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using TaskTracker.Database.Models;
using TaskTracker.Database.Models.Saps;
using TaskTracker.Database.Models.Task;

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
                Database.EnsureDeleted();
                Database.Migrate();
                //AddMockDataAsync(); -> will add later for seemless first time experience
            #else
                Database.Migrate();
            #endif
        }

        public virtual DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
        public virtual DbSet<ApplicationUserRole> ApplicationUserRoles => Set<ApplicationUserRole>();
        public virtual DbSet<TaskEntity> Tasks => Set<TaskEntity>();
        public virtual DbSet<TaskBlockerEntity> TaskBlockers => Set<TaskBlockerEntity>();
        public virtual DbSet<ApplicationUserPassword> ApplicationUserPasswords => Set<ApplicationUserPassword>();

        //saps    
        public virtual DbSet<CaseEntity> Cases=> Set<CaseEntity>();
        public virtual DbSet<StationEntity> Stations=> Set<StationEntity>();
        public virtual DbSet<SubjectEntity> Subjects=> Set<SubjectEntity>();
        public virtual DbSet<SubjectWarrantEntity> SubjectWarrants=> Set<SubjectWarrantEntity>();
        public virtual DbSet<DisappearanceDetailsEntity> DisappearanceDetails=> Set<DisappearanceDetailsEntity>();

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
            
        }
    }
}