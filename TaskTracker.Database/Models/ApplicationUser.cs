using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace TaskTracker.Database.Models
{
    public class ApplicationUser : Audit
    {
        public int Id { get; set; }
        public string FirstName{ get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public virtual ApplicationUserRole UserRole { get; set; }
    }
}
