namespace TaskTracker.Database.Models.Saps
{
    public class StationEntity
    {
        public int Id { get; set; }
        public string StationName { get; set; }
        public List<string> StationTelephoneLines { get; set; }

    }
}
