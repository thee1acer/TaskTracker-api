using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Database.Models.Saps;

namespace TaskTracker.Database.Configurations.Saps
{
    public class CaseEntityConfiguration : IEntityTypeConfiguration<CaseEntity>
    {
        public void Configure(EntityTypeBuilder<CaseEntity> builder)
        {
            builder.ToTable("Case");
            
            builder.HasKey(t => t.Id);

            builder.HasOne(x => x.Subject);
            builder.HasOne(x => x.Station);
        }
    }
}
