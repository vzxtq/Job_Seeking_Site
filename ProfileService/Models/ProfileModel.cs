namespace ProfileService.Models
{
    public class ProfileModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public RoleType Role { get; set; } = RoleType.Employee;
    }

    public enum RoleType
    {
        Employee,
        Employer,
    }
}