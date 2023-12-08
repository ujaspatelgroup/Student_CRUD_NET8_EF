using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCRUD.DTOs.Student;
using StudentCRUD.Models;

namespace StudentCRUD.Services.Student
{
    public class StudentService : IStudentService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StudentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetStudentDto>>> GetAllStudentsAsync()
        {
            var serviceresponse = new ServiceResponse<List<GetStudentDto>>();
            var students = await _context.Students.ToListAsync();
            serviceresponse.data = _mapper.Map<List<GetStudentDto>>(students);
            return serviceresponse;
        }

        public async Task<ServiceResponse<GetStudentDto>> GetStudentAsync(int id)
        {
            var serviceresponse = new ServiceResponse<GetStudentDto>();
            var findstudent = await FindStudent(id);
            if (findstudent is not null)
            {
                serviceresponse.data = _mapper.Map<GetStudentDto>(findstudent);
                return serviceresponse;
            }
            else
            {
                serviceresponse.Success = false;
                serviceresponse.Message = "Student not found";
                return serviceresponse;
            }
        }

        public async Task<ServiceResponse<List<GetStudentDto>>> AddStudentAsync(AddStudentDto student)
        {
            var serviceresponse = new ServiceResponse<List<GetStudentDto>>();
            _context.Students.Add(_mapper.Map<Students>(student));
            await _context.SaveChangesAsync();
            serviceresponse.data = _mapper.Map<List<GetStudentDto>>(await _context.Students.ToListAsync());
            return serviceresponse;
        }

        public async Task<ServiceResponse<GetStudentDto>> UpdateStudentAsync(UpdateStudentDto student)
        {
            var serviceresponse = new ServiceResponse<GetStudentDto>();
            var findstudent = await FindStudent(student.Id);
            if (findstudent is not null)
            {
                findstudent.Name = student.Name;
                findstudent.Address = student.Address;
                await _context.SaveChangesAsync();
                serviceresponse.data = _mapper.Map<GetStudentDto>(findstudent);
                return serviceresponse;
            }
            else
            {
                serviceresponse.Success = false;
                serviceresponse.Message = "Student not found";
                return serviceresponse;
            }
        }

        public async Task<ServiceResponse<GetStudentDto>> DeleteStudentAsync(int id)
        {
            var serviceresponse = new ServiceResponse<GetStudentDto>();
            var findstudent = await FindStudent(id);
            if (findstudent is not null)
            {
                _context.Students.Remove(_mapper.Map<Students>(findstudent));
                await _context.SaveChangesAsync();
                serviceresponse.data = _mapper.Map<GetStudentDto>(findstudent);
                return serviceresponse;
            }
            else
            {
                serviceresponse.Success = false;
                serviceresponse.Message = "Student not found";
                return serviceresponse;
            }
        }

        private async Task<Students> FindStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            return student;
        }
    }
}
