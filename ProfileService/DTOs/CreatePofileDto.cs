using ProfileService.Models;

namespace ProfileService.DTOs
{
    public class CreateProfile
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public RoleType Role { get; set; } = RoleType.Employee;
    }
}