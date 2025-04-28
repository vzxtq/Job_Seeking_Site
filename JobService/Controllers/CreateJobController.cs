
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

        public CreateJobController(JobDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob([FromBody] CreateJobDto createJobDto)
        {

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