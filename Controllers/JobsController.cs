using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobTracker.Api.Data;
using JobTracker.Api.Models;

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
}