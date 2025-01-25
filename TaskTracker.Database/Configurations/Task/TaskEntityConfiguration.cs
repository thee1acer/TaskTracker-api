using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Database.Models.Task;

namespace TaskTracker.Database.Configurations.Task
{
    public class TaskEntityConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.ToTable("TaskEntity");
            
            builder.HasKey(t => t.Id);

            builder.HasMany(x => x.TaskBlockers);
        }
    }
}
