namespace TaskTracker.Common.Models
{
    public class ApplicationUserPasswordDTO : AuditDTO
    {
        public Guid Id { get; set; }
        public string PasswordHash { get; set; }
        public bool Inactive { get; set; }
        public virtual ApplicationUserDTO ApplicationUser{ get; set; }
    }
}
