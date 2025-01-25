
using TaskTracker.Common.Enums;

namespace TaskTracker.Common.Models.Saps
{
    public class SubjectDTO
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

        public SubjectWarrantDTO? SubjectWarrant { get; set; } //if wanted
        public DisappearanceDetailsDTO? DisappearanceDetails { get; set; } //if missing
    }
}
