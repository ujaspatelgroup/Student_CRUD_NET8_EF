using System.ComponentModel.DataAnnotations;

namespace StudentCRUD.Models
{
    public class Students
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public required string FirstName { get; set; }

        [StringLength(50)]
        public required string LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Address { get; set; }

        public DateTime? DOB { get; set; }

        public string? Gender { get; set; }
    }
}
