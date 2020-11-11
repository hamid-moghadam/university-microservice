using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Identity;
using Microsoft.Extensions.Logging;
using Identity.API.Grpc;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Grpc
{
    public class UserGrpcService : UserService.UserServiceBase
    {
        private ILogger<UserGrpcService> _logger;

        private UserManager<ApplicationUser> _userManager;

        public UserGrpcService(ILogger<UserGrpcService> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public override async Task<UserInformationReply> Get(UserInformationRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Get user details of {0}", request.UserId);
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return new UserInformationReply
                {
                    Email = "user.Email",
                    Enable = true,
                    Username = "user.UserName",
                    FirstName = "user.FirstName",
                    LastName = "user.LastName",
                    NationalCode = "user.NationalCode",
                    PhoneNumber = "user.PhoneNumber"
                };
            return new UserInformationReply
            {
                Email = user.Email ?? "",
                Enable = user.Enable,
                Username = user.UserName ?? "",
                FirstName = user.FirstName ?? "",
                LastName = user.LastName ?? "",
                NationalCode = user.NationalCode ?? "",
                PhoneNumber = user.PhoneNumber ?? ""
            };
        }

        public override async Task<UserCreateReply> Create(UserCreateRequest request, ServerCallContext context)
        {
            var appUser = new ApplicationUser
            {
                Email = "",
                FirstName = request.FirstName,
                LastName = request.LastName,
                NationalCode = request.NationalCode,
                PhoneNumber = "",
                UserName = request.NationalCode
            };

            try
            {
                await _userManager.CreateAsync(appUser);

                await _userManager.AddPasswordAsync(appUser, appUser.NationalCode);
            }
            catch (Exception e)
            {
                _logger.LogInformation("error occured while creating user");
                return new UserCreateReply
                {
                    Email = "",
                    Enable = "false",
                    Id = "",
                    Success = false,
                    Username = "",
                    ErrorDescription = e.Message,
                    FirstName = "",
                    LastName = "",
                    NationalCode = "",
                    PhoneNumber = ""
                };
            }

            _logger.LogInformation("user {0} is being created", appUser.Id);
            return new UserCreateReply
            {
                Email = appUser.Email,
                Enable = "false",
                Id = appUser.Id,
                Success = true,
                Username = appUser.UserName,
                ErrorDescription = "",
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                NationalCode = appUser.NationalCode,
                PhoneNumber = appUser.PhoneNumber
            };
        }

        public override async Task<UserCreateReply> Update(UserUpdateRequest request, ServerCallContext context)
        {
            var appUser = await _userManager.FindByIdAsync(request.Id);
            if (appUser == null)
                return new UserCreateReply
                {
                    Email = "",
                    Enable = "false",
                    Id = "",
                    Success = false,
                    Username = "",
                    ErrorDescription = "User not found",
                    FirstName = "",
                    LastName = "",
                    NationalCode = "",
                    PhoneNumber = ""
                };

            appUser.FirstName = request.FirstName;
            appUser.LastName = request.LastName;
            appUser.NationalCode = request.NationalCode;

            try
            {
                await _userManager.UpdateAsync(appUser);
                await _userManager.RemovePasswordAsync(appUser);
                await _userManager.AddPasswordAsync(appUser, appUser.NationalCode);
            }
            catch (Exception e)
            {
                return new UserCreateReply
                {
                    Email = "",
                    Enable = "false",
                    Id = "",
                    Success = false,
                    Username = "",
                    ErrorDescription = e.Message,
                    FirstName = "",
                    LastName = "",
                    NationalCode = "",
                    PhoneNumber = ""
                };
            }

            _logger.LogInformation("User {0} successfully updated", appUser.Id);
            return new UserCreateReply
            {
                Email = appUser.Email,
                Enable = "false",
                Id = appUser.Id,
                Success = true,
                Username = appUser.UserName,
                ErrorDescription = "",
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                NationalCode = appUser.NationalCode,
                PhoneNumber = appUser.PhoneNumber
            };
        }


        public override async Task<UserRemoveReply> Remove(UserInformationRequest request, ServerCallContext context)
        {
            var appUser = await _userManager.FindByIdAsync(request.UserId);
            if (appUser == null)
                return new UserRemoveReply
                {
                    Success = false
                };

            try
            {
                await _userManager.DeleteAsync(appUser);
            }
            catch (Exception e)
            {
                _logger.LogInformation("error occured while deleting user {0}. exception: {1}", appUser.Id, e.Message);
                return new UserRemoveReply
                {
                    Success = false
                };
            }

            _logger.LogInformation("User {0} successfully were removed", request.UserId);

            return new UserRemoveReply
            {
                Success = true
            };
        }
    }
}