namespace TaskTracker.Database.Models
{
    public class ApplicationUserPassword : Audit
    {
        public Guid Id { get; set; }
        public string PasswordHash { get; set; }
        public bool Inactive { get; set; }
        public virtual ApplicationUser ApplicationUser{ get; set; }
    }
}
