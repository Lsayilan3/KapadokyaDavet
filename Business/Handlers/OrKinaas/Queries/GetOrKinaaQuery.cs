
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OrKinaas.Queries
{
    public class GetOrKinaaQuery : IRequest<IDataResult<OrKinaa>>
    {
        public int OrKinaaId { get; set; }

        public class GetOrKinaaQueryHandler : IRequestHandler<GetOrKinaaQuery, IDataResult<OrKinaa>>
        {
            private readonly IOrKinaaRepository _orKinaaRepository;
            private readonly IMediator _mediator;

            public GetOrKinaaQueryHandler(IOrKinaaRepository orKinaaRepository, IMediator mediator)
            {
                _orKinaaRepository = orKinaaRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OrKinaa>> Handle(GetOrKinaaQuery request, CancellationToken cancellationToken)
            {
                var orKinaa = await _orKinaaRepository.GetAsync(p => p.OrKinaaId == request.OrKinaaId);
                return new SuccessDataResult<OrKinaa>(orKinaa);
            }
        }
    }
}
