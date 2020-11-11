namespace Core.Events
{
    public interface ICurriculumCapacityFreed
    {
        int Id { get; set; }
        int CurrentCapacity { get; set; }
        int FieldId { get; set; }
    }

    public class CurriculumCapacityFreed : ICurriculumCapacityFreed
    {
        public int Id { get; set; }
        public int CurrentCapacity { get; set; }
        public int FieldId { get; set; }
    }
}