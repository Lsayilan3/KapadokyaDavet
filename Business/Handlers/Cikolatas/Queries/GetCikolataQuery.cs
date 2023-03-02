
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Cikolatas.Queries
{
    public class GetCikolataQuery : IRequest<IDataResult<Cikolata>>
    {
        public int CikolataId { get; set; }

        public class GetCikolataQueryHandler : IRequestHandler<GetCikolataQuery, IDataResult<Cikolata>>
        {
            private readonly ICikolataRepository _cikolataRepository;
            private readonly IMediator _mediator;

            public GetCikolataQueryHandler(ICikolataRepository cikolataRepository, IMediator mediator)
            {
                _cikolataRepository = cikolataRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Cikolata>> Handle(GetCikolataQuery request, CancellationToken cancellationToken)
            {
                var cikolata = await _cikolataRepository.GetAsync(p => p.CikolataId == request.CikolataId);
                return new SuccessDataResult<Cikolata>(cikolata);
            }
        }
    }
}
