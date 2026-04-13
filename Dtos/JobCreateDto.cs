namespace JobTracker.Api.Dtos;

public class JobCreateDto
{
    public string Company { get; set; }
    public string Position { get; set; }
    public string Status { get; set; }
    public string Location {get; set;}
}