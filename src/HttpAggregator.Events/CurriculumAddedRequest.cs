namespace HttpAggregator.Events
{
    public class CurriculumAddedRequest : ICurriculumAddedRequest
    {
        public int CurriculumId { get; set; }
        public string UserId { get; set; }
        public int StudentId { get; set; }
    }

    public interface ICurriculumAddedRequest
    {
        public int CurriculumId { get; set; }
        public string UserId { get; set; }
        public int StudentId { get; set; }
    }
}