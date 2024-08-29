using System.ComponentModel.DataAnnotations;

namespace BaseArchiCsharp.Domain.Dto;

public class UserDto
{
    public int Id { get; init; }

    [Required]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "Username must be between 1 and 20 characters long")]
    public string Username { get; init; } = "";

    [Required]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "Password must be between 1 and 20 characters long")]
    public string Password { get; init; } = "";

    [Required]
    [RegularExpression("^[^@]+@[^@]+$",
        ErrorMessage = "Invalid email format. Ensure there's at least one character before and after '@'.")]
    public string Email { get; init; } = "";
}