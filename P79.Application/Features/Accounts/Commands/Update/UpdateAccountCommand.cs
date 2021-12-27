using AutoMapper;
using MediatR;
using P79.Application.Exceptions;
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

namespace P79.Application.Features.Commands.Update
{
    public class UpdateAccountCommand : AccountDto,IRequest<Response<Unit>>
    {
        public int Id { get; set; }
    }

    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, Response<Unit>>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.Account> _genericRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateAccountCommandHandler(IGenericRepositoryAsync<Account> genericRepository, IMapper mapper, IMediator mediator)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Response<Unit>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var data = await _genericRepository.GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new ApiException("Data Account tidak ditemukan");
            }
            data.Name = request.Name;
            await _genericRepository.UpdateAsync(data);
            return new Response<Unit>(Unit.Value) { StatusCode = (int)HttpStatusCode.OK };
        }
    }
}
