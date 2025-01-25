using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Database.Models.Saps;

namespace TaskTracker.Database.Configurations.Saps
{
    public class SubjectEntityConfiguration : IEntityTypeConfiguration<SubjectEntity>
    {
        public void Configure(EntityTypeBuilder<SubjectEntity> builder)
        {
            builder.ToTable("Subject");

            builder.HasKey(t => t.Id);
        }
    }
}
