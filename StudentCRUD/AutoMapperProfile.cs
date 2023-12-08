using AutoMapper;
using StudentCRUD.DTOs.Student;
using StudentCRUD.Models;

namespace StudentCRUD
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Students, GetStudentDto>();
            CreateMap<AddStudentDto, Students>();
            CreateMap<Students, UpdateStudentDto>();
        }
    }
}
