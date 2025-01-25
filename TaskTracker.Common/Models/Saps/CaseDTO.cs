using TaskTracker.Common.Enums;

namespace TaskTracker.Common.Models.Saps
{
    public class CaseDTO : AuditDTO
    {
        public int Id { get; set; }
        public int CaseNumber { get; set; }
        public CaseStatusEnum CaseStatus { get; set; }
        public string CaseStatusDescription { get; set; } //why is it active and/or what is happening at the moment
        public string InvestigatingOfficer { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public SubjectDTO Subject { get; set; }
        public StationDTO Station { get; set; }
    }
}
