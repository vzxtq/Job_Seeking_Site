using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileService.DTOs;
using ProfileService.Models;

[ApiController]
[Route("api/profile")]
public class ProfileController : ControllerBase
{
    private readonly ProfileDbContext _context;
    private readonly ILogger<ProfileController> _logger;

    public ProfileController(ProfileDbContext context, ILogger<ProfileController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProfile([FromBody] CreateProfile request)
    {
        if (request == null)
        {
            return BadRequest("Profile data is invalid.");
        }

        var profile = new ProfileModel
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            Email = request.Email,
            Role = request.Role
        };

        _context.Profiles.Add(profile);
        await _context.SaveChangesAsync();

        var profileDto = new ProfileService.DTOs.Profile
        {
            Id = profile.Id,
            FullName = profile.FullName,
            Email = profile.Email,
            Role = profile.Role
        };

        return CreatedAtAction(nameof(GetProfile), new { id = profileDto.Id }, profileDto);
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult GetProfile()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                    User.FindFirst("sub")?.Value ??
                    User.FindFirst("unique_name")?.Value;

        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
        if (!roles.Any())
        {
            roles = User.FindAll("role").Select(r => r.Value).ToList();
        }

        return Ok(new
        {
            UserId = userId,
            Email = email,
            Roles = roles
        });
    }

    [HttpGet("applications")] 
    public async Task<IActionResult> GetApplications([FromServices] IHttpClientFactory httpClientFactory)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User ID not found");
        }

        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        _logger.LogInformation("User role" + userRole);

        var client = httpClientFactory.CreateClient();
        client.BaseAddress = new Uri("http://applicationservice:8080");

        HttpResponseMessage response;

        if (userRole != "Employer")
        {
            response = await client.GetAsync("api/applications?userId={userId}");
        }
        else
        {
            response = await client.GetAsync("api/applications");
        }

        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode, "Failed to fetch applications");
        }

        var content = await response.Content.ReadAsStringAsync();

        return Content(content, "application/json");
    }
}
