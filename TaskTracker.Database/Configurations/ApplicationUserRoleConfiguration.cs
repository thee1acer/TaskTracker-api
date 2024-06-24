using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Database.Models;

namespace TaskTracker.Database.Configurations
{
    public class ApplicationUserRoleConfiguration: IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            builder.ToTable("ApplicationUserRole");

            builder.HasKey(x => x.Id);

            builder.HasData(
                new ApplicationUserRole
                {
                   Id = 1,
                   Role = "Test User",
                   HasAdminRights = false
                },
                new ApplicationUserRole
                {
                    Id = 2,
                    Role = "QA Tester",
                    HasAdminRights = false
                },
                new ApplicationUserRole
                {
                    Id = 3,
                    Role = "Admin",
                    HasAdminRights = true
                }
            );
        }
    }
}
