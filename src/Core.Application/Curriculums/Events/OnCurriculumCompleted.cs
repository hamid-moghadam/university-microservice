using MediatR;

namespace Core.Application.Curriculums.Events
{
    public class OnCurriculumCompleted : INotification
    {
        public OnCurriculumCompleted(int curriculumId, int fieldId)
        {
            Id = curriculumId;
            FieldId = fieldId;
        }

        public int Id { get; }
        public int FieldId { get; }
    }
}