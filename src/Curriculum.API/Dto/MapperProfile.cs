using AutoMapper;
using Core.Events;
using Curriculum.API.Data.Models;
using Course = Core.Events.Course;
using Field = Core.Events.Field;
using Semester = Core.Events.Semester;
using Teacher = Core.Events.Teacher;

namespace Curriculum.API.Dto
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<StudentCurriculumDto, StudentCurriculum>().ReverseMap();

            CreateMap<CurriculumResponse, Data.Models.Curriculum>();

            CreateMap<Data.Models.Curriculum, CurriculumDto>();

            CreateMap<StudentResponse, Student>();

            CreateMap<Course, API.Data.Models.Course>();
            CreateMap<Core.Domain.Curriculum, API.Data.Models.Curriculum>();
            CreateMap<Field, API.Data.Models.Field>();
            CreateMap<Teacher, API.Data.Models.Teacher>();
            CreateMap<Semester, API.Data.Models.Semester>();
        }
    }
}