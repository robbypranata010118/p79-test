using AutoMapper;
using MediatR;
using P79.Base.Dtos.Accounts;
using P79.Base.Interfaces;
using P79.Base.Wrappers;
using P79.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P79.Application.Features.Queries.GetById
{
    public class GetAccountByIdQuery : IRequest<Response<AccountResponse>>
    {
        public int Id { get; set; }
    }

    public class GetAccountByIdQueryhandler : IRequestHandler<GetAccountByIdQuery, Response<AccountResponse>>
    {
        private readonly IGenericRepositoryAsync<Account> _genericRepository;
        private readonly IMapper _mapper;

        public GetAccountByIdQueryhandler(IGenericRepositoryAsync<Account> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response<AccountResponse>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _genericRepository.GetByIdAsync(request.Id, "Id", new string[]{});
            var dataVm = _mapper.Map<AccountResponse>(data);
            return new Response<AccountResponse>(dataVm);
        }
    }
}
