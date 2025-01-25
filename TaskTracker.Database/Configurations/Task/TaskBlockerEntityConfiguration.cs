using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Database.Models.Task;

namespace TaskTracker.Database.Configurations.Task
{
    public class TaskBlockerEntityConfiguration : IEntityTypeConfiguration<TaskBlockerEntity>
    {
        public void Configure(EntityTypeBuilder<TaskBlockerEntity> builder)
        {
            builder.ToTable(nameof(TaskBlockerEntity));
            
            builder.HasKey(t => t.Id);
        }
    }
}
