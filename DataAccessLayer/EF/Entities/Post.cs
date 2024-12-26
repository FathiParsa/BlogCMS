using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.EF.Entities;

public class Post
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;
    [StringLength(250)]
    public string? Content { get; set; } = null!;

    public int? AuthorId { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UploadedAt { get; set; } = DateTime.Now;

    public int? CategoryId { get; set; }

    public User? Author { get; set; }

    public Category? Category { get; set; }

    public ICollection<Comment>? Comments { get; set; } = new List<Comment>();
}
