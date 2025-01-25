using System.ComponentModel;

namespace TaskTracker.Common.Enums{
    public enum CaseStatusEnum
    {
        [Description("Resolved")]
        Resolved = 0,
        [Description("On-Hold")]
        OnHold = 1,
        [Description("Dismissed")]
        Dismissed = 2,
        [Description("Active")]
        Active = 3,
    }
}