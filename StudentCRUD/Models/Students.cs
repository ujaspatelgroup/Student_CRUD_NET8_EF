using System.ComponentModel.DataAnnotations;

namespace StudentCRUD.Models
{
    public class Students
    {
        public int Id { get; set; }

        [StringLength(50)]
        public required string Name { get; set; }

        public string? Address { get; set; }
    }
}
