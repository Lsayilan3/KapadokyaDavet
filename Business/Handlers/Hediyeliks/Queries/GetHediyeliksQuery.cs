
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

namespace Business.Handlers.Hediyeliks.Queries
{

    public class GetHediyeliksQuery : IRequest<IDataResult<IEnumerable<Hediyelik>>>
    {
        public class GetHediyeliksQueryHandler : IRequestHandler<GetHediyeliksQuery, IDataResult<IEnumerable<Hediyelik>>>
        {
            private readonly IHediyelikRepository _hediyelikRepository;
            private readonly IMediator _mediator;

            public GetHediyeliksQueryHandler(IHediyelikRepository hediyelikRepository, IMediator mediator)
            {
                _hediyelikRepository = hediyelikRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Hediyelik>>> Handle(GetHediyeliksQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Hediyelik>>(await _hediyelikRepository.GetListAsync());
            }
        }
    }
}