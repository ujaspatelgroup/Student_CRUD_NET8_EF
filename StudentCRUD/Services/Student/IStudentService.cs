using StudentCRUD.Models;

namespace StudentCRUD.Services.Student
{
    public interface IStudentService
    {
        public Task<List<Students>> GetAllStudentsAsync();

        public Task<Students> GetStudentAsync(int id);

        public Task<List<Students>> AddStudentAsync(Students student);

        public Task<Students> UpdateStudentAsync(Students student);

        public Task<Students> DeleteStudentAsync(int id);
    }
}
