using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.EF.Entities;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    [Required, StringLength(16)]
    public string Password { get; set; } = null!;

    //public int RoleId { get; set; }
    //public Role? Role { get; set; }

    public string? Email { get; set; } = null!;

    public DateTime? CreateAt { get; set; } = DateTime.Now;

    public DateTime? LastLogin { get; set; } = DateTime.Now;

    public ICollection<Comment>? Comments { get; set; } = new List<Comment>();

    public ICollection<MediaFile>? MediaFiles { get; set; } = new List<MediaFile>();

    public ICollection<Page>? Pages { get; set; } = new List<Page>();

    public ICollection<Post>? Posts { get; set; } = new List<Post>();
}
