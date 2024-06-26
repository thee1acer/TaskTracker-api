using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Database.Models;

namespace TaskTracker.Database.Configurations
{
    public class ApplicationUserPasswordConfiguration: IEntityTypeConfiguration<ApplicationUserPassword>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserPassword> builder)
        {
            builder.ToTable("ApplicationUserPassword");

            builder.HasKey(x => x.Id);               
        }
    }
}


