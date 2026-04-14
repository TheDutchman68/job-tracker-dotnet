using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobTracker.Api.Data;
using JobTracker.Api.Models;
using JobTracker.Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace JobTracker.Api.Controllers
{
    [ApiController]
    [Route("api/jobs")]
    [Authorize]
    public class JobsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JobsController(AppDbContext context)
        {
            _context = context;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userIdClaim);
        }

        [HttpGet]
        public async Task<ActionResult<List<Job>>> GetJobs()
        {
            var currentUserId = GetCurrentUserId();

            var jobs = await _context.Jobs
                .Where(j => j.UserId == currentUserId)
                .ToListAsync();

            return Ok(jobs);
        }

        [HttpPost]
        public async Task<ActionResult<Job>> CreateJob(JobCreateDto dto)
        {
            var currentUserId = GetCurrentUserId();

            var job = new Job
            {
                Company = dto.Company,
                Position = dto.Position,
                Status = dto.Status,
                Location = dto.Location,
                UserId = currentUserId
            };

            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJobs), new { id = job.Id}, job);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Job>> UpdateJob(int id, JobCreateDto dto)
        {
            var currentUserId = GetCurrentUserId();

            var job = await _context.Jobs
                    .FirstOrDefaultAsync(j => j.Id == id && j.UserId == currentUserId);

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
             var currentUserId = GetCurrentUserId();

              var job = await _context.Jobs
                    .FirstOrDefaultAsync(j => j.Id == id && j.UserId == currentUserId);

            if(job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }

}
