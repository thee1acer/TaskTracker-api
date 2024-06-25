using System.ComponentModel;

namespace TaskTracker.Enums
{
    public enum TaskTypeEnum
    {
        [Description("User Story")]
        UserStory = 1,

        [Description("Bug")]
        Bug = 2,

        [Description("Production Defect")]
        ProductionDefect = 3,

        [Description("Spike")]
        Spike = 4,
    }
}
