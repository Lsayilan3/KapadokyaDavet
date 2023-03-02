
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OrDuguns.Queries
{
    public class GetOrDugunQuery : IRequest<IDataResult<OrDugun>>
    {
        public int OrDugunId { get; set; }

        public class GetOrDugunQueryHandler : IRequestHandler<GetOrDugunQuery, IDataResult<OrDugun>>
        {
            private readonly IOrDugunRepository _orDugunRepository;
            private readonly IMediator _mediator;

            public GetOrDugunQueryHandler(IOrDugunRepository orDugunRepository, IMediator mediator)
            {
                _orDugunRepository = orDugunRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OrDugun>> Handle(GetOrDugunQuery request, CancellationToken cancellationToken)
            {
                var orDugun = await _orDugunRepository.GetAsync(p => p.OrDugunId == request.OrDugunId);
                return new SuccessDataResult<OrDugun>(orDugun);
            }
        }
    }
}
