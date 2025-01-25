using TaskTracker.Common.Enums;

namespace TaskTracker.Database.Models.Saps
{
    public class CaseEntity : Audit
    {
        public int Id { get; set; }
        public int CaseNumber { get; set; }
        public CaseStatusEnum CaseStatus { get; set; }
        public string CaseStatusDescription { get; set; } //why is it active and/or what is happening at the moment
        public string InvestigatingOfficer { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public SubjectEntity Subject { get; set; }
        public StationEntity Station { get; set; }
    }
}