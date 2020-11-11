using Core.Application.Dto.Curriculum;
using MediatR;

namespace Core.Application.Curriculums.Events
{
    public class OnCurriculumUpdated : INotification
    {
        public OnCurriculumUpdated(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}