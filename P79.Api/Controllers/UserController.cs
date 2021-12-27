using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GaransiBank.Application.Features.Users.Queries.GetUserByIdQueryAdmin;
using GaransiBank.Application.Features.Users.Queries.GetUserQueryAdmin;
using System.Collections.Generic;
using System.Threading.Tasks;
using GaransiBank.Base.Wrappers;
using GaransiBank.Application.Features.Users.Commands.Create;
using GaransiBank.Application.Features.Users.Commands.Update;
using GaransiBank.Application.Features.Users.Commands.Delete;
using GaransiBank.Application;
using GaransiBank.Base.Dtos.User;
using System;

namespace GaransiBank.Api.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class UserController : BaseApiController
    {
        [HttpPost("Get")]
        [ProducesResponseType(type: typeof(Response<IEnumerable<GetUserViewModel>>), statusCode: StatusCodes.Status200OK)]
        [AppAuthorize(Feature = "User", Permissions = Constant.CanView)]
        public async Task<IActionResult> Get([FromBody] GetUserQueryAdmin query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(type: typeof(Response<GetUserViewModel>), statusCode: StatusCodes.Status200OK)]
        [AppAuthorize(Feature = "User", Permissions = Constant.CanView)]
        public async Task<IActionResult> GetById([FromRoute] GetUserByIdQueryAdmin query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [AppAuthorize(Feature = "User", Permissions = Constant.CanAdd)]
        public async Task<IActionResult> Create([FromBody] CreateUserCommandAdmin command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPut("{Id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [AppAuthorize(Feature = "User", Permissions = Constant.CanEdit)]
        public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] PutUserAdminVm command)
        {
            UpdateUserCommandAdmin updateUser = new UpdateUserCommandAdmin
            {
                Id = Id,
                Email = command.Email,
                IsActive = command.IsActive,
                Name = command.Name,
                NIK = command.NIK,
                Role = command.Role,
                Title = command.Title,
                Unit = command.Unit
            };
            return Ok(await Mediator.Send(updateUser));
        }
        [HttpDelete("{Id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [AppAuthorize(Feature = "User", Permissions = Constant.CanDelete)]
        public async Task<IActionResult> Delete([FromRoute] DeleteUserCommandAdmin command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
