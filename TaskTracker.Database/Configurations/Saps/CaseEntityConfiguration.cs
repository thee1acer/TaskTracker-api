using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskTracker.Database.Models.Saps
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
