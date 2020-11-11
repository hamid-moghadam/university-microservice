using MediatR;

namespace Core.Application.Curriculums.Events
{
    public class OnCurriculumFreed : INotification
    {
        public OnCurriculumFreed(int curriculumId, int currentCapacity, int fieldId)
        {
            Id = curriculumId;
            CurrentCapacity = currentCapacity;
            FieldId = fieldId;
        }

        public int Id { get; }

        public int CurrentCapacity { get; }
        
        public int FieldId { get; }
    }
}