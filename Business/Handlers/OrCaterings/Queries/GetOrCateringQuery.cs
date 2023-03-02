
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OrCaterings.Queries
{
    public class GetOrCateringQuery : IRequest<IDataResult<OrCatering>>
    {
        public int OrCateringId { get; set; }

        public class GetOrCateringQueryHandler : IRequestHandler<GetOrCateringQuery, IDataResult<OrCatering>>
        {
            private readonly IOrCateringRepository _orCateringRepository;
            private readonly IMediator _mediator;

            public GetOrCateringQueryHandler(IOrCateringRepository orCateringRepository, IMediator mediator)
            {
                _orCateringRepository = orCateringRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OrCatering>> Handle(GetOrCateringQuery request, CancellationToken cancellationToken)
            {
                var orCatering = await _orCateringRepository.GetAsync(p => p.OrCateringId == request.OrCateringId);
                return new SuccessDataResult<OrCatering>(orCatering);
            }
        }
    }
}
