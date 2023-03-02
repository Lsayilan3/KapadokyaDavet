
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.OrPikniks.Queries
{

    public class GetOrPikniksQuery : IRequest<IDataResult<IEnumerable<OrPiknik>>>
    {
        public class GetOrPikniksQueryHandler : IRequestHandler<GetOrPikniksQuery, IDataResult<IEnumerable<OrPiknik>>>
        {
            private readonly IOrPiknikRepository _orPiknikRepository;
            private readonly IMediator _mediator;

            public GetOrPikniksQueryHandler(IOrPiknikRepository orPiknikRepository, IMediator mediator)
            {
                _orPiknikRepository = orPiknikRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OrPiknik>>> Handle(GetOrPikniksQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OrPiknik>>(await _orPiknikRepository.GetListAsync());
            }
        }
    }
}