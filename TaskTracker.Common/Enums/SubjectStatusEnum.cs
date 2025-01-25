using System.ComponentModel;

namespace TaskTracker.Common.Enums{
    public enum SubjectStatusEnum
    {
        [Description("Deceased")]
        Deceased = 0,
        [Description("Found")]
        Found = 1,
        [Description("Missing")]
        Missing = 2,
        [Description("Wanted")]
        Wanted = 3,
        [Description("Apprehended")]
        Apprehended = 4,
    }
}