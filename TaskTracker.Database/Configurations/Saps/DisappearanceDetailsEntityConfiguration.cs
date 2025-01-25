using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskTracker.Database.Models.Saps
{
    public class DisappearanceDetailsEntityConfiguration : IEntityTypeConfiguration<DisappearanceDetailsEntity>
    {
        public void Configure(EntityTypeBuilder<DisappearanceDetailsEntity> builder)
        {
            builder.ToTable("DisappearanceDetails");

            builder.HasKey(t => t.Id);
        }
    }
}
