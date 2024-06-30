namespace TaskTracker.Common.Models.Task
{
    public class TaskEntityDTO : AuditDTO
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
        public virtual List<TaskBlockerEntityDTO>? TaskBlockers { get; set; }

    }
}
