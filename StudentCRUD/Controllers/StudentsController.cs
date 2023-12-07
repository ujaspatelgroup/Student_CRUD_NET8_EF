using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentCRUD.Models;
using StudentCRUD.Services.Student;

namespace StudentCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<List<Students>> Get()
        {
            return await _studentService.GetAllStudentsAsync();
        }

        [HttpGet("{id}")]
        public async Task<Students> Get(int id)
        {
            return await _studentService.GetStudentAsync(id);
        }

        [HttpPost]
        public async Task<List<Students>> AddStudent(Students student)
        {
            return await _studentService.AddStudentAsync(student);
        }

        [HttpPut]
        public async Task<Students> UpdateStudent(Students student)
        {
            return await _studentService.UpdateStudentAsync(student);
        }

        [HttpDelete]
        public async Task<Students> DeleteStudent(int id)
        {
            return await _studentService.DeleteStudentAsync(id);
        }
    }
}
