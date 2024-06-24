using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskTracker.Database.Models.Task
{
    public class TaskBlockerEntityConfiguration : IEntityTypeConfiguration<TaskBlockerEntity>
    {
        public void Configure(EntityTypeBuilder<TaskBlockerEntity> builder)
        {
            builder.ToTable(nameof(TaskBlockerEntity));
            
            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.OriginalTask);
        }
    }
}
