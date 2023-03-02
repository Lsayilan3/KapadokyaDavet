
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

namespace Business.Handlers.Yiyeceks.Queries
{

    public class GetYiyeceksQuery : IRequest<IDataResult<IEnumerable<Yiyecek>>>
    {
        public class GetYiyeceksQueryHandler : IRequestHandler<GetYiyeceksQuery, IDataResult<IEnumerable<Yiyecek>>>
        {
            private readonly IYiyecekRepository _yiyecekRepository;
            private readonly IMediator _mediator;

            public GetYiyeceksQueryHandler(IYiyecekRepository yiyecekRepository, IMediator mediator)
            {
                _yiyecekRepository = yiyecekRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Yiyecek>>> Handle(GetYiyeceksQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Yiyecek>>(await _yiyecekRepository.GetListAsync());
            }
        }
    }
}