using System.ComponentModel.DataAnnotations;

public class User
{
    public Guid Id { get; set; }

    public required string FullName { get; set; } = string.Empty;

    [EmailAddress]
    public required string Email { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters.")]
    public required string Password { get; set; } = string.Empty;
    public required RoleType Role { get; set; } = RoleType.Employee;
}
public enum RoleType
{
    Employee,
    Employer
};
