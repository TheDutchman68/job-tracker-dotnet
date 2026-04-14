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
        public async Task<ActionResult<List<Job>>> GetJobs([FromQuery] string? status, [FromQuery] string? search)
        {
            var currentUserId = GetCurrentUserId();

            var query = _context.Jobs
                .Where(j => j.UserId == currentUserId)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status) &&
                Enum.TryParse<JobStatus>(status, true, out var parsedStatus))
            {
                query = query.Where(j => j.Status == parsedStatus);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var loweredSearch = search.ToLower();

                query = query.Where(j =>
                    j.Company.ToLower().Contains(loweredSearch) ||
                    j.Position.ToLower().Contains(loweredSearch));
            }

            var jobs = await query
                .OrderByDescending(j => j.CreatedAt)
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
