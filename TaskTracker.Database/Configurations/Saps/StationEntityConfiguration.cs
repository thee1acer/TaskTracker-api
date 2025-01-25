using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Database.Models.Saps;

namespace TaskTracker.Database.Configurations.Saps
{
    public class StationEntityConfiguration : IEntityTypeConfiguration<StationEntity>
    {
        public void Configure(EntityTypeBuilder<StationEntity> builder)
        {
            builder.ToTable("Station");

            builder.HasKey(t => t.Id);
        }
    }
}
