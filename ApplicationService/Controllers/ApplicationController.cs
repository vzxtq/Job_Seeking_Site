using System.CodeDom.Compiler;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/applications")]
public class ApplicationController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ApplicationController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitApplication([FromBody] ApplicationDto applicationDto)
    {
        if (applicationDto == null)
        {
            return BadRequest("Application data is required.");
        }

        var application = new ApplicationModel
        {
            FullName = applicationDto.FullName,
            Email = applicationDto.Email,
            Resume = applicationDto.Resume,
            Country = applicationDto.Country
        };

        await _context.Applications.AddAsync(application);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Application submitted successfully." });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllApplications()
    {
        var applications = await _context.Applications.ToListAsync();
        return Ok(applications);
    }
}