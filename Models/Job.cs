namespace JobTracker.Api.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public JobStatus Status { get; set; }
        public string Location { get; set;}
        public int UserId { get; set; }

        public User User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}