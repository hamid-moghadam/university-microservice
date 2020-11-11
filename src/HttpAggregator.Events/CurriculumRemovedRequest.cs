namespace HttpAggregator.Events
{
    public interface ICurriculumRemovedRequest
    {
        int CurriculumId { get; set; }
        string UserId { get; set; }
        int StudentId { get; set; }
    }

    public class CurriculumRemovedRequest : ICurriculumRemovedRequest
    {
        public int CurriculumId { get; set; }
        public string UserId { get; set; }
        public int StudentId { get; set; }
    }
}