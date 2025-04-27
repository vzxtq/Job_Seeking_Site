using ProfileService.Models;

namespace ProfileService.DTOs
{
    public class Profile
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public RoleType Role { get; set; } = RoleType.Employee;
    }
}