using MediatR;

namespace Core.Application.Dto.Field.Events
{
    public class OnFieldUpdated : INotification
    {
        public OnFieldUpdated(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}