using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.EF.Entities
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string? Name { get; set; }
        [StringLength(250)]
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
