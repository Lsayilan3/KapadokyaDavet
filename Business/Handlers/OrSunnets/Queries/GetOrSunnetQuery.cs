
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OrSunnets.Queries
{
    public class GetOrSunnetQuery : IRequest<IDataResult<OrSunnet>>
    {
        public int OrSunnetId { get; set; }

        public class GetOrSunnetQueryHandler : IRequestHandler<GetOrSunnetQuery, IDataResult<OrSunnet>>
        {
            private readonly IOrSunnetRepository _orSunnetRepository;
            private readonly IMediator _mediator;

            public GetOrSunnetQueryHandler(IOrSunnetRepository orSunnetRepository, IMediator mediator)
            {
                _orSunnetRepository = orSunnetRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OrSunnet>> Handle(GetOrSunnetQuery request, CancellationToken cancellationToken)
            {
                var orSunnet = await _orSunnetRepository.GetAsync(p => p.OrSunnetId == request.OrSunnetId);
                return new SuccessDataResult<OrSunnet>(orSunnet);
            }
        }
    }
}
