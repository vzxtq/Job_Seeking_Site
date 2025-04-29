
using System.Security.Claims;
using JobService.DTOs;
using JobService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobService.Controllers
{

    [ApiController]
    [Route("api/job")]
    public class CreateJobController : ControllerBase
    {
        private readonly JobDbContext _context;
        private readonly ILogger<CreateJobController> _logger;
        public CreateJobController(JobDbContext context, ILogger<CreateJobController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob([FromBody] CreateJobDto createJobDto)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            _logger.LogInformation("User role" + userRole);
            if (userRole != "Employer")
            {
                return Forbid("You do not have permission to create this job.");
            }
            var job = new JobModel
            {
                Id = Guid.NewGuid(),
                Title = createJobDto.Title,
                Description = createJobDto.Description,
                Company = createJobDto.Company,
                Location = createJobDto.Location,
                EmploymentType = createJobDto.EmploymentType,
                Salary = createJobDto.Salary,
                Requirements = createJobDto.Requirements,
                Responsibilities = createJobDto.Responsibilities
            };

            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return Ok(new {jobId = job.Id});
        }

    }
}