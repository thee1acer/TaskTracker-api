namespace TaskTracker.Database.Models.Saps
{
     public class DisappearanceDetailsEntity
    {
        public int Id { get; set; }
        public string DisappearanceAge {  get; set; } 
        public string MissingCircumstances { get; set; }
        public DateTime MissingDate { get; set; }
        public string CirculationNumber { get; set; }

    }
}