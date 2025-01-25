using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Database.Models.Saps;

namespace TaskTracker.Database.Configurations.Saps
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
