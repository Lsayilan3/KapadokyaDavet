
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OrCikolatas.Queries
{
    public class GetOrCikolataQuery : IRequest<IDataResult<OrCikolata>>
    {
        public int OrCikolataId { get; set; }

        public class GetOrCikolataQueryHandler : IRequestHandler<GetOrCikolataQuery, IDataResult<OrCikolata>>
        {
            private readonly IOrCikolataRepository _orCikolataRepository;
            private readonly IMediator _mediator;

            public GetOrCikolataQueryHandler(IOrCikolataRepository orCikolataRepository, IMediator mediator)
            {
                _orCikolataRepository = orCikolataRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OrCikolata>> Handle(GetOrCikolataQuery request, CancellationToken cancellationToken)
            {
                var orCikolata = await _orCikolataRepository.GetAsync(p => p.OrCikolataId == request.OrCikolataId);
                return new SuccessDataResult<OrCikolata>(orCikolata);
            }
        }
    }
}
