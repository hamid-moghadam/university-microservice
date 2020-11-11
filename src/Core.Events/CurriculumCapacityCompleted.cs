namespace Core.Events
{
    public interface ICurriculumCapacityCompleted
    {
        int CurriculumId { get; set; }
        int FieldId { get; set; }
    }

    public class CurriculumCapacityCompleted : ICurriculumCapacityCompleted
    {
        public int CurriculumId { get; set; }
        public int FieldId { get; set; }
    }
}