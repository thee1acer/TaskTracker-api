namespace TaskTracker.Common.Models
{
    public class ApplicationUserRoleDTO
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public bool HasAdminRights { get; set; }
    }
}
