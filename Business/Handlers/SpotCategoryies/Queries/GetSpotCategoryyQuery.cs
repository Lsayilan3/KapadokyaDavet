
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.SpotCategoryies.Queries
{
    public class GetSpotCategoryyQuery : IRequest<IDataResult<SpotCategoryy>>
    {
        public int SpotCategoryyId { get; set; }

        public class GetSpotCategoryyQueryHandler : IRequestHandler<GetSpotCategoryyQuery, IDataResult<SpotCategoryy>>
        {
            private readonly ISpotCategoryyRepository _spotCategoryyRepository;
            private readonly IMediator _mediator;

            public GetSpotCategoryyQueryHandler(ISpotCategoryyRepository spotCategoryyRepository, IMediator mediator)
            {
                _spotCategoryyRepository = spotCategoryyRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<SpotCategoryy>> Handle(GetSpotCategoryyQuery request, CancellationToken cancellationToken)
            {
                var spotCategoryy = await _spotCategoryyRepository.GetAsync(p => p.SpotCategoryyId == request.SpotCategoryyId);
                return new SuccessDataResult<SpotCategoryy>(spotCategoryy);
            }
        }
    }
}
