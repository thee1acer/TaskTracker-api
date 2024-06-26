namespace TaskTracker.Common.Models
{
    public class ApplicationUserDTO : AuditDTO
    {
        public int Id { get; set; }
        public string FirstName{ get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Inactive { get; set; }
        public string? UnhashedPassword { get; set; }
        public string? Token { get; set; }
        public DateTime? TokenExpiry { get; set; }

        public virtual ApplicationUserPasswordDTO UserPassword { get; set; }
    }
}
