using System.Threading.Tasks;
using Core.Application.Dto.User;

namespace Core.Application.Services
{
    public interface IUserGrpcService
    {
        Task<UserDto> GetAsync(string id);
        Task<UserDto> CreateAsync(CreateUserDto dto);
        Task<UserDto> UpdateAsync(string id, CreateUserDto dto);
        Task RemoveAsync(string id);
    }
}