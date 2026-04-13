using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobTracker.Api.Data;
using JobTracker.Api.Models;
using JobTracker.Api.Dtos;

namespace JobTracker.Api.Controllers;

[ApiController]
[Route("api/jobs")]
public class JobsController : ControllerBase
{
    private readonly AppDbContext _context;

    public JobsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Job>>> GetJobs()
    {
        var jobs = await _context.Jobs.ToListAsync();
        return Ok(jobs);
    }

    [HttpPost]
    public async Task<ActionResult<Job>> CreateJob(JobCreateDto dto)
    {
        var job = new Job
        {
            Company = dto.Company,
            Position = dto.Position,
            Status = dto.Status,
            Location = dto.Location
        };

        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetJobs), new { id = job.Id}, job);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Job>> UpdateJob(int id, JobCreateDto dto)
    {
        var job = await _context.Jobs.FindAsync(id);

        if (job == null)
        {
            return NotFound();
        }

        job.Company = dto.Company;
        job.Position = dto.Position;
        job.Status = dto.Status;
        job.Location = dto.Location;

        await _context.SaveChangesAsync();

        return Ok(job);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteJob(int id)
    {
        var job = await _context.Jobs.FindAsync(id);

        if(job == null)
        {
            return NotFound();
        }

        _context.Jobs.Remove(job);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}