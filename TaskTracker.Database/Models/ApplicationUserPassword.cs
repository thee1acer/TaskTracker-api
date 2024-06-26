using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Database.Models
{
    public class ApplicationUserPassword : Audit
    {
        [Key]
        public Guid Id { get; set; }
        public string PasswordHash { get; set; }
        public bool Inactive { get; set; }
    }
}
