using Kasp.Data.Models.Helpers;

namespace Core.Domain.Interfaces
{
    public interface IFullModel : IModel, IUpdateTime, ICreateTime
    {
    }
}