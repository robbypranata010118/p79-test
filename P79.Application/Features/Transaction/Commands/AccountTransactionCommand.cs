using AutoMapper;
using MediatR;
using P79.Base.Dtos.Transactions;
using P79.Base.Interfaces;
using P79.Base.Wrappers;
using P79.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P79.Application.Features.Transaction.Commands
{
    public class AccountTransactionCommand : TransactionDto , IRequest<Response<Unit>>
    {
    }

    public class AccountTransactionCommandHandler : IRequestHandler<AccountTransactionCommand, Response<Unit>>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.Transaction> _genericRepository;
        private readonly IGenericRepositoryAsync<Domain.Entities.AccountPoint> _accountPointRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AccountTransactionCommandHandler(IGenericRepositoryAsync<Domain.Entities.Transaction> genericRepository, IMapper mapper, IMediator mediator ,IGenericRepositoryAsync<AccountPoint> accountPointRepository)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _mediator = mediator;
            _accountPointRepository = accountPointRepository;
        }

        public async Task<Response<Unit>> Handle(AccountTransactionCommand request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<Domain.Entities.Transaction>(request);
            await _genericRepository.AddAsync(data);
            var point = 0;
            //point only for Debit
            if (request.DebitCreditStatus == "D")
            {
                switch (request.Description)
                {
                    case "Bayar Listrik":
                        point = CalculatePoint(request.Amount, 2000);
                        break;
                    case "Beli Pulsa":
                        point = CalculatePoint(request.Amount, 1000);
                        break;
                    default:
                        break;
                }
            }
            
            if ((request.Description == "Bayar Listrik" || request.Description == "Beli Pulsa") && request.DebitCreditStatus == "D")
            {
                var hasPoint = await _accountPointRepository.GetByPredicate(x => x.AccountPointId == request.AccountId);
                if (hasPoint != null)
                {
                    hasPoint.Point += point;
                    await _accountPointRepository.UpdateAsync(hasPoint);
                }
                else
                {
                    var accountPoint = new Domain.Entities.AccountPoint
                    {
                        AccountPointId = request.AccountId,
                        Point = point,
                        DateIn = DateTime.Now,
                        UserIn = "System"
                    };
                    await _accountPointRepository.AddAsync(accountPoint);
                }
            }
            
            return new Response<Unit>(Unit.Value) { StatusCode = (int)HttpStatusCode.Created };
        }

        private int CalculatePoint(decimal amount , int divide)
        {
            var point = 0;
            if (amount > 50000)
            {
                point += (int)(50000 / divide) * 1;
            }
            if (amount > 100000)
            {
                point += (int)(((amount - 100000) / divide) * 2);
            }
            return point;
        }
    }

   
}


