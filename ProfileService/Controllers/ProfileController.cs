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

    public ProfileController(ProfileDbContext context)
    {
        _context = context;
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
}
