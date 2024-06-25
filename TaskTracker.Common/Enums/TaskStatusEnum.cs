using System.ComponentModel;

namespace TaskTracker.Common.Enums
{
    public enum TaskStatusEnum
    {
        [Description("New")]
        New = 1,
        [Description("In-Progress")]
        InProgress = 2,
        [Description("On-Hold")]
        OnHold = 3,
        [Description("Done")]
        Done = 4,
        [Description("Deffered")]
        Deffered = 5,
        [Description("Removed")]
        Removed = 6,
    }
}
