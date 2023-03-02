
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OrAnimasyones.Queries
{
    public class GetOrAnimasyoneQuery : IRequest<IDataResult<OrAnimasyone>>
    {
        public int OrAnimasyoneId { get; set; }

        public class GetOrAnimasyoneQueryHandler : IRequestHandler<GetOrAnimasyoneQuery, IDataResult<OrAnimasyone>>
        {
            private readonly IOrAnimasyoneRepository _orAnimasyoneRepository;
            private readonly IMediator _mediator;

            public GetOrAnimasyoneQueryHandler(IOrAnimasyoneRepository orAnimasyoneRepository, IMediator mediator)
            {
                _orAnimasyoneRepository = orAnimasyoneRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OrAnimasyone>> Handle(GetOrAnimasyoneQuery request, CancellationToken cancellationToken)
            {
                var orAnimasyone = await _orAnimasyoneRepository.GetAsync(p => p.OrAnimasyoneId == request.OrAnimasyoneId);
                return new SuccessDataResult<OrAnimasyone>(orAnimasyone);
            }
        }
    }
}
