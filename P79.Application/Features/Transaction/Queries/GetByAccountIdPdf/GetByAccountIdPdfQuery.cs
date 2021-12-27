using MediatR;
using P79.Base.Dtos.Transactions;
using P79.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P79.Application.Features.Transaction.Queries.GetByAccountIdPdf
{
    public class GetByAccountIdPdfQuery : TransactionByAccountIdRequest, IRequest<byte[]>
    {
    }
    public class GetByAccountIdPdfQueryHandler : IRequestHandler<GetByAccountIdPdfQuery, byte[]>
    {
        private readonly ITransactionByAccountId _transactionByAccountId;
        private readonly IPdfService _pdfService;

        public GetByAccountIdPdfQueryHandler(ITransactionByAccountId transactionByAccountId, IPdfService pdfService)
        {
            _transactionByAccountId = transactionByAccountId;
            _pdfService = pdfService;
        }

        public async Task<byte[]> Handle(GetByAccountIdPdfQuery request, CancellationToken cancellationToken)
        {
            var data = await _transactionByAccountId.GetTransactionByAccountId(request);
            var html = $"<table border='1'><thead><tr><td>Transaction Date</td><td>Description</td><td>Credit</td><td>Debit</td><td>Amount</td></tr></thead><tbody>";
            foreach (var item in data)
            {
                html += $"<tr><td>{item.TransactionDate.ToString("yyyy-MM-dd")}</td><td>{item.Description}</td><td>{item.Credit}</td><td>{item.Debit}</td><td>{item.Amount}</td></tr>";
            }
            html += "</tbody></table>";
            var pdf = await _pdfService.GeneratePdf(html);
            return pdf;
        }
    }
}
