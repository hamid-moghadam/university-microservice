using MediatR;

 namespace Core.Application.Helpers {
	public interface IAuthenticatedRequest<T> : IRequest<T> {
		public string UserId { get; }
	}
}
