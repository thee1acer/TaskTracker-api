namespace TaskTracker.Common.Models.Saps
{
     public class DisappearanceDetailsDTO
    {
        public int Id { get; set; }
        public string DisappearanceAge {  get; set; } 
        public string MissingCircumstances { get; set; }
        public DateTime MissingDate { get; set; }
        public string CirculationNumber { get; set; }

    }
}