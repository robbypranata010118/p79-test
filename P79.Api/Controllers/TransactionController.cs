using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P79.Application.Features.Transaction.Commands;
using P79.Application.Features.Transaction.Queries.GetByAccountId;
using P79.Application.Features.Transaction.Queries.GetByAccountIdPdf;
using System.Threading.Tasks;

namespace P79.Api.Controllers
{
    public class TransactionController : BaseApiController
    {
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ILogger<TransactionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetTransactionByAccountIdQuery command)
        {

            return Ok(await Mediator.Send(command));
        }

        [HttpGet("download-pdf")]
        public async Task<IActionResult> DownloadPdf([FromQuery] GetByAccountIdPdfQuery query)
        {
            var file = await Mediator.Send(query);
            return File
            (
                fileContents: file,
                contentType: "application/pdf",
                fileDownloadName: "Transaction.pdf"
            );
        }

        [HttpPost]
        public async Task<IActionResult> Post(AccountTransactionCommand command)
        {

            return Ok(await Mediator.Send(command));
        }
    }

   
}
