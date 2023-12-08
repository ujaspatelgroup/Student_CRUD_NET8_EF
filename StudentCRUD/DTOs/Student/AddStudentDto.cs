using System.ComponentModel.DataAnnotations;

namespace StudentCRUD.DTOs.Student
{
    public class AddStudentDto
    {
        [Required(ErrorMessage = "Student name is required")]
        public required string Name { get; set; }

        public string? Address { get; set; }
    }
}
