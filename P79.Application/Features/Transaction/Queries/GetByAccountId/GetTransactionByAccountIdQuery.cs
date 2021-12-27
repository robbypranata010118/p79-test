using MediatR;
using P79.Base.Dtos.Transactions;
using P79.Base.Interfaces;
using P79.Base.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P79.Application.Features.Transaction.Queries.GetByAccountId
{
    public class GetTransactionByAccountIdQuery : TransactionByAccountIdRequest , IRequest<PagedResponse<IEnumerable<TransactionByAccountIdResponse>>>
    {
    }

    public class GetTransactionByAccountIdQueryHandler : IRequestHandler<GetTransactionByAccountIdQuery, PagedResponse<IEnumerable<TransactionByAccountIdResponse>>>
    {
        private readonly ITransactionByAccountId _transactionByAccountId;

        public GetTransactionByAccountIdQueryHandler(ITransactionByAccountId transactionByAccountId)
        {
            _transactionByAccountId = transactionByAccountId;
        }

        public async Task<PagedResponse<IEnumerable<TransactionByAccountIdResponse>>> Handle(GetTransactionByAccountIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _transactionByAccountId.GetTransactionByAccountId(request);

            return new PagedResponse<IEnumerable<TransactionByAccountIdResponse>>(data, new PagedInfoRepositoryResponse
            {
                CurrentPage = 1,
                Length = -1,
                TotalPage = 1
            }, 1, -1)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
