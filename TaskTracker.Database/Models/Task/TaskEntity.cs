namespace TaskTracker.Database.Models.Task
{
    public class TaskEntity : Audit
    {
        public int Id { get; set; }
        public int AssignedTo {  get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }
        public string State { get; set; }
        public string TaskType { get; set; }
        public int TaskPriority { get; set; }
        public int TaskStoryPoints { get; set; }
        public int TaskStoryEffort { get; set; }
        public virtual List<TaskBlockerEntity> TaskBlockers { get; set; }

    }
}
