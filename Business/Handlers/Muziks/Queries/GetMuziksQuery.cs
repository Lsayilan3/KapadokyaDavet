
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

namespace Business.Handlers.Muziks.Queries
{

    public class GetMuziksQuery : IRequest<IDataResult<IEnumerable<Muzik>>>
    {
        public class GetMuziksQueryHandler : IRequestHandler<GetMuziksQuery, IDataResult<IEnumerable<Muzik>>>
        {
            private readonly IMuzikRepository _muzikRepository;
            private readonly IMediator _mediator;

            public GetMuziksQueryHandler(IMuzikRepository muzikRepository, IMediator mediator)
            {
                _muzikRepository = muzikRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Muzik>>> Handle(GetMuziksQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Muzik>>(await _muzikRepository.GetListAsync());
            }
        }
    }
}