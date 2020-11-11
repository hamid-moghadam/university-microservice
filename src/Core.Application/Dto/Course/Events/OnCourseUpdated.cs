using MediatR;

namespace Core.Application.Dto.Course.Events
{
    public class OnCourseUpdated : INotification
    {
        public OnCourseUpdated(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}