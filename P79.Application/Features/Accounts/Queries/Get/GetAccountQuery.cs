using AutoMapper;
using MediatR;
using P79.Base.Dtos.Accounts;
using P79.Base.Interfaces;
using P79.Base.Parameters;
using P79.Base.Wrappers;
using P79.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P79.Application.Features.Queries.Get
{
    public class GetAccountQuery : RequestParameter, IRequest<PagedResponse<IEnumerable<AccountResponse>>>
    {
    }

    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, PagedResponse<IEnumerable<AccountResponse>>>
    {
        private readonly IGenericRepositoryAsync<Account> _genericRepository;
        private readonly IMapper _mapper;

        public GetAccountQueryHandler(IGenericRepositoryAsync<Account> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<AccountResponse>>> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            var includes = new string[] { };
            var data = await _genericRepository.GetPagedReponseAsync(request, includes);
            var dataVm = _mapper.Map<IEnumerable<AccountResponse>>(data.Results);
            return new PagedResponse<IEnumerable<AccountResponse>>(dataVm, data.Info, request.Page, request.Length)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
