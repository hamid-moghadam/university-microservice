using Core.Domain.Enums;

namespace Core.Application.Dto.Field
{
    public class FieldDto
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public DegreeType DegreeType { get; set; }

        public int FieldGroupId { get; set; }
    }
}