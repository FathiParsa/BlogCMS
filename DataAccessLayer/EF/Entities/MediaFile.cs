namespace DataAccessLayer.EF.Entities;

public class MediaFile
{
    public int Id { get; set; }

    public string FileName { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public int? UploadedBy { get; set; }

    public DateTime? UploadedAt { get; set; } = DateTime.Now;

    public User? UploadedByNavigation { get; set; }
}
