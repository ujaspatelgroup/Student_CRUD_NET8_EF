using System.ComponentModel.DataAnnotations;

namespace StudentCRUD.DTOs.Student
{
    public class GetStudentDto
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public string? Address { get; set; }

        public string? Email { get; set; }

        public DateTime? DOB { get; set; }

        public string? Gender { get; set; }
    }
}