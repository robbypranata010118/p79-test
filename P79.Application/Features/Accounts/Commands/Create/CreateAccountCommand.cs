using AutoMapper;
using MediatR;
using P79.Base.Dtos.Accounts;
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

namespace P79.Application.Features.Commands.Create
{
    public class CreateAccountCommand : AccountDto,IRequest<Response<Unit>>
    {

    }

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Response<Unit>>
    {
        private readonly IGenericRepositoryAsync<Account> _genericRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateAccountCommandHandler(IGenericRepositoryAsync<Account> genericRepository, IMapper mapper, IMediator mediator)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Response<Unit>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<Account>(request);
            await _genericRepository.AddAsync(data);
            return new Response<Unit>(Unit.Value) { StatusCode = (int)HttpStatusCode.Created };
        }
    }
}
