using System.ComponentModel.DataAnnotations;

namespace StudentCRUD.DTOs.Student
{
    public class AddStudentDto
    {
        
        [StringLength(50)]
        [Required(ErrorMessage = "Student first name is required")]
        public required string FirstName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Student last name is required")]
        public required string LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Address { get; set; }

        public DateTime? DOB { get; set; }

        public string? Gender { get; set; }
    }
}
