using System.ComponentModel.DataAnnotations;

namespace BaseArchiCsharp.Domain.Common;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
}