
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

namespace Business.Handlers.Ensonuruns.Queries
{

    public class GetEnsonurunsQuery : IRequest<IDataResult<IEnumerable<Ensonurun>>>
    {
        public class GetEnsonurunsQueryHandler : IRequestHandler<GetEnsonurunsQuery, IDataResult<IEnumerable<Ensonurun>>>
        {
            private readonly IEnsonurunRepository _ensonurunRepository;
            private readonly IMediator _mediator;

            public GetEnsonurunsQueryHandler(IEnsonurunRepository ensonurunRepository, IMediator mediator)
            {
                _ensonurunRepository = ensonurunRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Ensonurun>>> Handle(GetEnsonurunsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Ensonurun>>(await _ensonurunRepository.GetListAsync());
            }
        }
    }
}