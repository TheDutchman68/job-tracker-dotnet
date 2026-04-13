namespace JobTracker.Api.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string Status { get; set; }
        public string Location { get; set;}
    }
}