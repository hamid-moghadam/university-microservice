namespace Curriculum.API.Dto.Grpc
{
    public class SemesterStatusDto
    {
        public bool IsCurriculumSemesterValid { get; set; }
        public bool CanTakeCurriculums { get; set; }
    }
}