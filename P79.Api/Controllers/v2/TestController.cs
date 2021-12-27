using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using GaransiBank.Application.Features.Test.Commands.SendEmailVerification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaransiBank.Api.Admin.Controllers.v2
{
    [Authorize]
    [ApiVersion("2.0")]
    public class TestController : BaseApiController
    {
        [HttpPost("send-email")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Create([FromBody] SendEmailVerificationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
