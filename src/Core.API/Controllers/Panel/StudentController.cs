using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Dto.Student;
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
using Microsoft.Extensions.Configuration;

namespace Core.API.Controllers.Panel
{
    [EntityManagerInfo("دانشجویان", Name = "students")]
    public class StudentController : PanelApiController<Student, IStudentService,
        StudentPartialDto,
        StudentPartialDto,
        StudentEditDto, FilterBase>
    {
        private readonly IConfiguration _configuration;
        private readonly IUserGrpcService _userGrpcService;

        public StudentController(IStudentService repository, IObjectMapper objectMapper, IFormBuilder builder,
            IConfiguration configuration, IUserGrpcService userGrpcService) : base(repository, objectMapper, builder)
        {
            _configuration = configuration;
            _userGrpcService = userGrpcService;
        }

        public override async Task<ActionResult<StudentPartialDto>> Get(int id)
        {
            var result = await this.Repository.GetAsync<StudentPartialDto>(id, new CancellationToken());
            var user = await _userGrpcService.GetAsync(result.UserId);
            result = user.MapTo(result);
            return Ok(result);
        }

        public override async Task<ActionResult<PagedResult<StudentPartialDto>>> List(FilterBase filter)
        {
            var result = await base.List(filter);

            for (var i = 0; i < result.Value.Items.Length; i++)
            {
                var user = await _userGrpcService.GetAsync(result.Value.Items[i].UserId);
                result.Value.Items[i] = user.MapTo(result.Value.Items[i]);
            }

            return result;
        }

        public override async Task<ActionResult<StudentPartialDto>> Create(StudentEditDto model)
        {
            var item = model.MapTo<Student>();
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

            ActionResult<StudentPartialDto> actionResult = CreatedAtAction("Get", new
            {
                id = item.Id
            }, item);
            return actionResult;
        }

        public override async Task<ActionResult<StudentPartialDto>> Update(int id, StudentEditDto dto)
        {
            var student = await Repository.GetAsync(id, new CancellationToken());
            student = _objectMapper.MapTo(dto, student);
            var oldUserInformation = await _userGrpcService.GetAsync(student.UserId);
            await _userGrpcService.UpdateAsync(student.UserId, new CreateUserDto
            {
                Firstname = dto.Firstname,
                LastName = dto.LastName,
                NationalCode = dto.NationalCode
            });
            try
            {
                await Repository.UpdateAsync(student, new CancellationToken());
            }
            catch (Exception e)
            {
                await _userGrpcService.UpdateAsync(student.UserId, new CreateUserDto
                {
                    Firstname = oldUserInformation.Firstname,
                    LastName = oldUserInformation.LastName,
                    NationalCode = oldUserInformation.NationalCode
                });
                throw;
            }

            ActionResult<StudentPartialDto> actionResult =
                student.MapTo<StudentPartialDto>();
            student = default;
            return actionResult;
        }

        public override async Task<IActionResult> Delete(int id)
        {
            var student = await Repository.GetAsync(id);
            if (student != null)
            {
                await _userGrpcService.RemoveAsync(student.UserId);
                await Repository.RemoveAsync(id, new CancellationToken());
            }

            return NoContent();
        }
    }
}