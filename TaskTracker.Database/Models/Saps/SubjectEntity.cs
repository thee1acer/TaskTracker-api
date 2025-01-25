
using TaskTracker.Common.Enums;

namespace TaskTracker.Database.Models.Saps
{
    public class SubjectEntity
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? Surname  { get; set; } 
        public SubjectStatusEnum Status { get; set; } 
        public List<string>? Alias { get; set; } 
        public string Gender { get; set; }
        public string? EyeColor { get; set; }
        public string? HairColor { get; set; }
        public string Height {  get; set; } 
        public string Weight { get; set; } 
        public string Build {  get; set; } 
        public string SubjectPhoto { get; set; }

        public SubjectWarrantEntity? SubjectWarrant { get; set; } //if wanted
        public DisappearanceDetailsEntity? DisappearanceDetails { get; set; } //if missing
    }
}
