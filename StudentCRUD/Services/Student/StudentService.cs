using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCRUD.Models;

namespace StudentCRUD.Services.Student
{
    public class StudentService : IStudentService
    {

        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Students>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Students> GetStudentAsync(int id)
        {
            var student = await FindStudent(id);
            if (student is not null)
            {
                return student;
            }
            else
            {
                throw new Exception("Student not found");
            }
        }

        public async Task<List<Students>> AddStudentAsync(Students student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return await _context.Students.ToListAsync();
        }

        public async Task<Students> UpdateStudentAsync(Students student)
        {

            if (FindStudent(student.Id) is not null)
            {
                _context.Students.Update(student);
                await _context.SaveChangesAsync();
                return student;
            }
            else
            {
                throw new Exception("Student not found");
            }
        }

        public async Task<Students> DeleteStudentAsync(int id)
        {
            var student = await FindStudent(id);
            if (student is not null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return student;
            }
            else
            {
                throw new Exception("Student not found");
            }
        }

        private async Task<Students> FindStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            return student;
        }
    }
}
