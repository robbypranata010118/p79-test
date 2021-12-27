using AutoMapper;
using MediatR;
using P79.Application.Exceptions;
using P79.Base.Interfaces;
using P79.Base.Wrappers;
using P79.Domain.Entities;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace P79.Application.Features.Commands.Deactive
{
    public class DeactiveAccountCommand : IRequest<Response<Unit>>
    {
        public int Id { get; set; }
    }

    public class DeactiveAccountCommandHandler : IRequestHandler<DeactiveAccountCommand, Response<Unit>>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.Account> _generericRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DeactiveAccountCommandHandler(IGenericRepositoryAsync<Account> generericRepository, IMapper mapper, IMediator mediator)
        {
            _generericRepository = generericRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Response<Unit>> Handle(DeactiveAccountCommand request, CancellationToken cancellationToken)
        {
            var data = await _generericRepository.GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new ApiException("Data Account tidak ditemukan");
            }
            data.IsActive = false;
            await _generericRepository.UpdateAsync(data);
            return new Response<Unit>(Unit.Value) { StatusCode = (int)HttpStatusCode.OK };
        }
    }
}
