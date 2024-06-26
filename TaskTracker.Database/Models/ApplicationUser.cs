namespace TaskTracker.Database.Models
{
    public class ApplicationUser : Audit
    {
        public int Id { get; set; }
        public string FirstName{ get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Inactive { get; set; }

        public virtual ApplicationUserPassword UserPassword { get; set; }
    }
}
