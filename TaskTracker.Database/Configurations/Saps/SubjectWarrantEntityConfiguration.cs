
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Database.Models.Saps;

namespace TaskTracker.Database.Configurations.Saps
{
    public class SubjectWarrantEntityConfiguration : IEntityTypeConfiguration<SubjectWarrantEntity>
    {
        public void Configure(EntityTypeBuilder<SubjectWarrantEntity> builder)
        {
            builder.ToTable("SubjectWarrant");

            builder.HasKey(t => t.Id);
        }
    }
}
