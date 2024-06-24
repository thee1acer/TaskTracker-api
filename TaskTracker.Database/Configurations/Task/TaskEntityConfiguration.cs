using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskTracker.Database.Models.Task
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
