using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.EF.Entities;

public class Page
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    [StringLength(250)]
    public string? Content { get; set; } = null!;

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UploadedAt { get; set; } = DateTime.Now;

    public int? AuthorId { get; set; }

    public User? Author { get; set; } = null!;
}
