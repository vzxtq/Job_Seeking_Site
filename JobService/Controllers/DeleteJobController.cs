using System.Security.Claims;
using JobService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace JobService.Controllers
{
    [ApiController]
    [Route("api/job")]
    public class DeleteJobController : ControllerBase
    {
        private readonly JobDbContext _context;
        public DeleteJobController(JobDbContext context)
        {
            _context = context;
        }

        [HttpDelete("{jobId}")]
        public async Task<IActionResult> DeleteJob(Guid jobId)
        {
            var job = await _context.Jobs.FindAsync(jobId);
            if (job == null)
            {
                return NotFound("Job not found.");
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return Ok("Job deleted successfully.");
        }
    }
}