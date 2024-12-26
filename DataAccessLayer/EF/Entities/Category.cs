using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.EF.Entities;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [StringLength(200)]
    public string? Description { get; set; }

    public ICollection<Post>? Posts { get; set; } = new List<Post>();
}
