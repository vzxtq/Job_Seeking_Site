using JobService.DTOs;
using JobService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobService.Controllers
{
    [ApiController]
    [Route("api/job")]
    public class UpdateJobController : ControllerBase
    {
        private readonly JobDbContext _context;
        private readonly IConfiguration _configuration;

        public UpdateJobController(JobDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPut("{jobId}")]
        public async Task<IActionResult> UpdateJob(Guid jobId, [FromBody] UpdateJobDto updateJobDto)
        {
            var job = await _context.Jobs.FindAsync(jobId);
            if (job == null)
            {
                return NotFound("Job not found.");
            }

            job.Title = updateJobDto.Title ?? job.Title;
            job.Description = updateJobDto.Description ?? job.Description;
            job.Company = updateJobDto.Company ?? job.Company;
            job.Location = updateJobDto.Location ?? job.Location;
            job.EmploymentType = updateJobDto.EmploymentType ?? job.EmploymentType;
            job.Salary = updateJobDto.Salary.HasValue && updateJobDto.Salary.Value != 0 ? updateJobDto.Salary.Value : job.Salary;
            job.Requirements = updateJobDto.Requirements ?? job.Requirements;
            job.Responsibilities = updateJobDto.Responsibilities ?? job.Responsibilities;

            await _context.SaveChangesAsync();

            return Ok("Job updated successfully.");
        }
    }
}