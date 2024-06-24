using Microsoft.EntityFrameworkCore;

namespace TaskTracker.Database.Models
{
    public class ApplicationUserRole
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public bool HasAdminRights { get; set; }
    }
}
