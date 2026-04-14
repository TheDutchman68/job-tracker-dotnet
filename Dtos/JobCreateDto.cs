using JobTracker.Api.Models;
using System.ComponentModel.DataAnnotations;
namespace JobTracker.Api.Dtos;

public class JobCreateDto
{
    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string Company { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string Position { get; set; }

    [Required]
    [EnumDataType(typeof(JobStatus))]
    public JobStatus Status { get; set; }
    
    [MaxLength(100)]
    public string Location {get; set;}
}