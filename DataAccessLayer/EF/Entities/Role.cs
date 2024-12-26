using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.EF.Entities;

public class Role
{
    [Key]
    public int Id { get; set; }
    [Required , StringLength(100)]
    public string RoleName { get; set; } = null!;
}