using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Dto.Teacher;
using Core.Application.Dto.User;
using Core.Application.Services;
using Core.Domain;
using Kasp.Data;
using Kasp.Data.Models;
using Kasp.FormBuilder.Services;
using Kasp.ObjectMapper;
using Kasp.ObjectMapper.Extensions;
using Kasp.Panel.EntityManager;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers.Panel
{
    [EntityManagerInfo("اساتید", Name = "teachers")]
    public class TeacherController : PanelApiController<Teacher, ITeacherService,
        TeacherPartialDto,
        TeacherPartialDto,
        TeacherEditDto, FilterBase>
    {
        private readonly IUserGrpcService _userGrpcService;

        public TeacherController(ITeacherService repository, IObjectMapper objectMapper, IFormBuilder builder,
            IUserGrpcService userGrpcService) : base(
            repository, objectMapper, builder)
        {
            _userGrpcService = userGrpcService;
        }


        public override async Task<ActionResult<TeacherPartialDto>> Get(int id)
        {
            var result = await this.Repository.GetAsync<TeacherPartialDto>(id, new CancellationToken());
            var user = await _userGrpcService.GetAsync(result.UserId);
            result = user.MapTo(result);
            return Ok(result);
        }

        public override async Task<ActionResult<TeacherPartialDto>> Create(TeacherEditDto model)
        {
            var item = model.MapTo<Teacher>();
            var userResult = await _userGrpcService.CreateAsync(new CreateUserDto
            {
                Firstname = model.Firstname,
                LastName = model.LastName,
                NationalCode = model.NationalCode
            });
            item.UserId = userResult.Id;
            item.FullName = $"{userResult.Firstname} {userResult.LastName}";

            try
            {
                await Repository.AddAsync(item, new CancellationToken());
            }
            catch (Exception e)
            {
                await _userGrpcService.RemoveAsync(userResult.Id);
                throw;
            }

            ActionResult<TeacherPartialDto> actionResult = CreatedAtAction("Get", new
            {
                id = item.Id
            }, item);
            return actionResult;
        }

        public override async Task<ActionResult<PagedResult<TeacherPartialDto>>> List(FilterBase filter)
        {
            var result = await base.List(filter);

            for (var i = 0; i < result.Value.Items.Length; i++)
            {
                var user = await _userGrpcService.GetAsync(result.Value.Items[i].UserId);
                result.Value.Items[i] = user.MapTo(result.Value.Items[i]);
            }

            return result;
        }

        public override async Task<ActionResult<TeacherPartialDto>> Update(int id, TeacherEditDto dto)
        {
            var teacher = await Repository.GetAsync(id, new CancellationToken());
            teacher = _objectMapper.MapTo(dto, teacher);
            var oldUserInformation = await _userGrpcService.GetAsync(teacher.UserId);
            await _userGrpcService.UpdateAsync(teacher.UserId, new CreateUserDto
            {
                Firstname = dto.Firstname,
                LastName = dto.LastName,
                NationalCode = dto.NationalCode
            });
            try
            {
                await Repository.UpdateAsync(teacher, new CancellationToken());
            }
            catch (Exception e)
            {
                await _userGrpcService.UpdateAsync(teacher.UserId, new CreateUserDto
                {
                    Firstname = oldUserInformation.Firstname,
                    LastName = oldUserInformation.LastName,
                    NationalCode = oldUserInformation.NationalCode
                });
                throw;
            }

            ActionResult<TeacherPartialDto> actionResult =
                teacher.MapTo<TeacherPartialDto>();
            teacher = default;
            return actionResult;
        }

        public override async Task<IActionResult> Delete(int id)
        {
            var teacher = await Repository.GetAsync(id);
            if (teacher != null)
            {
                await _userGrpcService.RemoveAsync(teacher.UserId);
                await Repository.RemoveAsync(id, new CancellationToken());
            }

            return NoContent();
        }
    }
}