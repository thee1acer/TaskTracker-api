
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskTracker.Database.Models.Saps
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
