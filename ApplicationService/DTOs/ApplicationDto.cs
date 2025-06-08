public class ApplicationDto
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string Country { get; set; }
    public required string Resume { get; set; } // Mock
    public string? CoverLetter { get; set; } 
}