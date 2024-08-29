using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseArchiCsharp.Domain.Common;

namespace BaseArchiCsharp.Domain.Entities;

[Table("Users")]
public sealed class User : BaseEntity
{
    [Required]
    [MaxLength(50)]
    public string Username { get; init; } = "";
    
    [Required]
    [MaxLength(100)]
    public string Password { get; init; } = "";
    
    [Required]
    [MaxLength(150)]
    public string Email { get; init; } = "";
}