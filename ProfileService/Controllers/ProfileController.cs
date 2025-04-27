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
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProfile([FromRoute] Guid id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized("User not authenticated.");
        }

        var profile = await _context.Profiles.FindAsync(id);
        if (profile == null)
        {
            return NotFound("Profile not found.");
        }

        if (profile.Id.ToString() != userId && User.IsInRole("Admin") == false)
        {
            return Forbid("You do not have permission to access this profile.");
        }

        var profileDto = new ProfileService.DTOs.Profile
        {
            Id = profile.Id,
            FullName = profile.FullName,
            Email = profile.Email,
            Role = profile.Role
        };

        return Ok(profileDto);
    }

}