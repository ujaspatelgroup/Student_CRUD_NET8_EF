﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentCRUD.DTOs.Shared;
using StudentCRUD.DTOs.Student;
using StudentCRUD.Services.Student;

namespace StudentCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ServiceResponse<List<GetStudentDto>>> Get()
        {
            return await _studentService.GetAllStudentsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ServiceResponse<GetStudentDto>> Get(int id)
        {
            return await _studentService.GetStudentAsync(id);
        }

        [HttpPost]
        public async Task<ServiceResponse<List<GetStudentDto>>> AddStudent(AddStudentDto student)
        {
            return await _studentService.AddStudentAsync(student);
        }

        [HttpPut]
        public async Task<ServiceResponse<GetStudentDto>> UpdateStudent(UpdateStudentDto student)
        {
            return await _studentService.UpdateStudentAsync(student);
        }

        [HttpDelete("{id}")]
        public async Task<ServiceResponse<GetStudentDto>> DeleteStudent(int id)
        {
            return await _studentService.DeleteStudentAsync(id);
        }
    }
}
