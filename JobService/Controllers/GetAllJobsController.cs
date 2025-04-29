using JobService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobService.Controllers
{
    [ApiController]
    [Route("api/job")]
    public class GetAllJobsController : ControllerBase
    {
        private readonly JobDbContext _context;

        public GetAllJobsController(JobDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _context.Jobs.ToListAsync();
            if (jobs == null || !jobs.Any())
            {
                return NotFound("No jobs found.");
            }

            return Ok(jobs);
        }
    }
}