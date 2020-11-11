using System;
using System.Threading.Tasks;
using Core.Application.Dto.User;
using Core.Application.Services;
using Grpc.Identity;
using Grpc.Net.Client;
using Kasp.HttpException.Core;
using Microsoft.Extensions.Configuration;

namespace Core.Infrastructure.Services
{
    public class UserGrpcGrpcService : IUserGrpcService
    {
        private readonly IConfiguration _configuration;

        public UserGrpcGrpcService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<UserDto> GetAsync(string id)
        {
            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(_configuration["UserGrpc"]);
            var client = new UserService.UserServiceClient(channel);
            var user = await client.GetAsync(new UserInformationRequest {UserId = id});
            return new UserDto
            {
                Firstname = user.FirstName,
                LastName = user.LastName,
                NationalCode = user.NationalCode
            };
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(_configuration["UserGrpc"]);
            var client = new UserService.UserServiceClient(channel);
            var user = await client.CreateAsync(new UserCreateRequest
            {
                Email = "",
                Username = dto.NationalCode,
                FirstName = dto.Firstname,
                LastName = dto.LastName,
                NationalCode = dto.NationalCode,
                PhoneNumber = ""
            });
            if (!user.Success)
                throw new BadRequestException($"Error occured while creating user: {user.ErrorDescription}");
            return new UserDto
            {
                Id = user.Id,
                Firstname = user.FirstName,
                LastName = user.LastName,
                NationalCode = user.NationalCode
            };
        }

        public async Task<UserDto> UpdateAsync(string id, CreateUserDto dto)
        {
            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(_configuration["UserGrpc"]);
            var client = new UserService.UserServiceClient(channel);

            var result = await client.UpdateAsync(new UserUpdateRequest
            {
                Id = id,
                FirstName = dto.Firstname,
                LastName = dto.LastName,
                NationalCode = dto.NationalCode
            });
            if (!result.Success)
                throw new BadRequestException($"Error occured while updating user: {result.ErrorDescription}");

            return new UserDto
            {
                Firstname = result.FirstName,
                Id = result.Id,
                LastName = result.LastName,
                NationalCode = result.NationalCode
            };
        }

        public async Task RemoveAsync(string id)
        {
            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(_configuration["UserGrpc"]);
            var client = new UserService.UserServiceClient(channel);

            var result = await client.RemoveAsync(new UserInformationRequest
            {
                UserId = id
            });
            if (!result.Success)
                throw new BadRequestException($"Error occured while updating user");
        }
    }
}