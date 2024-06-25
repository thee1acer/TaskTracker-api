namespace TaskTracker.Common.Models.Task
{
    public class TaskBlockerEntityDTO : AuditDTO
    {
        public int Id { get; set; }
        public int OriginalTaskId { get; set; }
        public string BlockerReason {  get; set; }
        public bool InActive {  get; set; }
    }
}
