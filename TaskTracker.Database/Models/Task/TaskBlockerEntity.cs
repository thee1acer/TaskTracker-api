namespace TaskTracker.Database.Models.Task
{
    public class TaskBlockerEntity : Audit
    {
        public int Id { get; set; }
        public int OriginalTaskId { get; set; }
        public string BlockerReason {  get; set; }
        public bool InActive {  get; set; }
        public virtual TaskEntity  OriginalTask { get; set; }
    }
}
