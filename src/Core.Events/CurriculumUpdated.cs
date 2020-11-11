namespace Core.Events
{
    public interface ICurriculumUpdated
    {
        CurriculumResponse Curriculum { get; set; }
    }

    public class CurriculumUpdated : ICurriculumUpdated
    {
        public CurriculumResponse Curriculum { get; set; }
    }
}