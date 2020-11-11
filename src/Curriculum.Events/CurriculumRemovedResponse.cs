using System;

namespace Curriculum.Events
{
    public interface ICurriculumRemovedResponse
    {
        int CurriculumId { get; set; }
    }

    public class CurriculumRemovedResponse : ICurriculumRemovedResponse
    {
        public int CurriculumId { get; set; }
    }
}