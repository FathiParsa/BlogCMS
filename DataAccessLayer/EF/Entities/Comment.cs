using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.EF.Entities;

public class Comment
{
    public int Id { get; set; }

    public int? PostId { get; set; }

    public int? UserId { get; set; }

    [StringLength(250)]
    public string Content { get; set; } = null!;

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public Post? Post { get; set; }

    public User? User { get; set; }
}
