using StudentCRUD.DTOs.Shared;
using StudentCRUD.DTOs.Student;

namespace StudentCRUD.Services.Student
{
    public interface IStudentService
    {
        public Task<ServiceResponse<List<GetStudentDto>>> GetAllStudentsAsync();

        public Task<ServiceResponse<GetStudentDto>> GetStudentAsync(int id);

        public Task<ServiceResponse<List<GetStudentDto>>> AddStudentAsync(AddStudentDto student);

        public Task<ServiceResponse<GetStudentDto>> UpdateStudentAsync(UpdateStudentDto student);

        public Task<ServiceResponse<GetStudentDto>> DeleteStudentAsync(int id);
    }
}
