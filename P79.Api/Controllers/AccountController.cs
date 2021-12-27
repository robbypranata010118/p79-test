using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P79.Application.Features.Commands.Active;
using P79.Application.Features.Commands.Create;
using P79.Application.Features.Commands.Deactive;
using P79.Application.Features.Commands.Delete;
using P79.Application.Features.Commands.Update;
using P79.Application.Features.Queries.Get;
using P79.Application.Features.Queries.GetById;
using P79.Base.Dtos.Accounts;
using P79.Base.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace P79.Api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }
        /// <remarks>
        /// List Paramater:
        /// 
        ///     Avaiable Comparasion Operator
        ///     [        
        ///       like,
        ///       not like,
        ///       !=,
        ///       &lt;,
        ///       &gt;,
        ///       &lt;=,
        ///       &gt;=,
        ///       =
        ///     ]
        ///     Avaiable Column
        ///     [        
        ///       Id,
        ///       Name
        ///     ]
        ///     Avaiable Sorting Type
        ///     [        
        ///       ASC,
        ///       DESC,
        ///     ]        
        /// </remarks>
        [HttpPost("GetData")]
        [ProducesResponseType(type: typeof(PagedResponse<IEnumerable<AccountResponse>>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetData(GetAccountQuery filter)
        {
            //var keys = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(filter)));
            //if (_cache.TryGetValue(keys, out PagedResponse<IEnumerable<AccountResponse>> Accounts))
            //{
            //    return Ok(Accounts);
            //}
            //var cacheOptions = new MemoryCacheEntryOptions()
            //{
            //    AbsoluteExpiration = DateTime.Now.AddMinutes(1),
            //    Priority = CacheItemPriority.High
            //};
            //cacheOptions.RegisterPostEvictionCallback(
            //(key, value, reason, substate) =>
            //{
            //    Console.Write("Cache expired!");
            //});
            //var dataAccounts = await Mediator.Send(filter);
            //_cache.Set(keys, dataAccounts, cacheOptions);
            return Ok(await Mediator.Send(filter));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(type: typeof(Response<AccountResponse>), statusCode: StatusCodes.Status200OK)]
        //[AppAuthorize(Feature = Constant.FEATURE_BRANCH, Permissions = Constant.CanView)]
        public async Task<IActionResult> Get(int id)
        {
            //var keys = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(id)));
            //if (_cache.TryGetValue(keys, out Response<AccountResponse> Accounts))
            //{
            //    return Ok(Accounts);
            //}
            //var dataAccount = await Mediator.Send(new GetAccountByIdQuery { Id = id });
            //var cacheOptions = new MemoryCacheEntryOptions()
            //{
            //    AbsoluteExpiration = DateTime.Now.AddMinutes(1),
            //    Priority = CacheItemPriority.High
            //};
            //cacheOptions.RegisterPostEvictionCallback(
            //(key, value, reason, substate) =>
            //{
            //    Console.Write("Cache expired!");
            //});
            //_cache.Set(keys, dataAccount, cacheOptions);
            return Ok(await Mediator.Send(new GetAccountByIdQuery { Id = id }));
        }
        [HttpPost]
        //[AppAuthorize(Feature = Constant.FEATURE_BRANCH, Permissions = Constant.CanAdd)]
        public async Task<IActionResult> Post(CreateAccountCommand command)
        {

            return Ok(await Mediator.Send(command));
        }
        [HttpPut]
        //[AppAuthorize(Feature = Constant.FEATURE_BRANCH, Permissions = Constant.CanEdit)]
        public async Task<IActionResult> Put(UpdateAccountCommand command)
        {

            return Ok(await Mediator.Send(command));
        }
        [HttpPut("deactivate/{id:int}")]
        //[AppAuthorize(Feature = Constant.FEATURE_BRANCH, Permissions = Constant.CanEdit)]
        public async Task<IActionResult> Deactivate(int id)
        {

            return Ok(await Mediator.Send(new DeactiveAccountCommand { Id = id }));
        }
        [HttpDelete("{id:int}")]
        //[AppAuthorize(Feature = Constant.FEATURE_BRANCH, Permissions = Constant.CanDelete)]
        public async Task<IActionResult> Delete(int id)
        {

            return Ok(await Mediator.Send(new DeleteAccountCommand { Id = id }));
        }
        [HttpPut("activate/{id:int}")]
        //[AppAuthorize(Feature = Constant.FEATURE_BRANCH, Permissions = Constant.CanEdit)]
        public async Task<IActionResult> Activate(int id)
        {

            return Ok(await Mediator.Send(new ActiveAccountCommand { Id = id }));
        }
    }
}
