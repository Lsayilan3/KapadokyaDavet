
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OrKokteyls.Queries
{
    public class GetOrKokteylQuery : IRequest<IDataResult<OrKokteyl>>
    {
        public int OrKokteylId { get; set; }

        public class GetOrKokteylQueryHandler : IRequestHandler<GetOrKokteylQuery, IDataResult<OrKokteyl>>
        {
            private readonly IOrKokteylRepository _orKokteylRepository;
            private readonly IMediator _mediator;

            public GetOrKokteylQueryHandler(IOrKokteylRepository orKokteylRepository, IMediator mediator)
            {
                _orKokteylRepository = orKokteylRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OrKokteyl>> Handle(GetOrKokteylQuery request, CancellationToken cancellationToken)
            {
                var orKokteyl = await _orKokteylRepository.GetAsync(p => p.OrKokteylId == request.OrKokteylId);
                return new SuccessDataResult<OrKokteyl>(orKokteyl);
            }
        }
    }
}
