
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

namespace Business.Handlers.OrNisans.Queries
{

    public class GetOrNisansQuery : IRequest<IDataResult<IEnumerable<OrNisan>>>
    {
        public class GetOrNisansQueryHandler : IRequestHandler<GetOrNisansQuery, IDataResult<IEnumerable<OrNisan>>>
        {
            private readonly IOrNisanRepository _orNisanRepository;
            private readonly IMediator _mediator;

            public GetOrNisansQueryHandler(IOrNisanRepository orNisanRepository, IMediator mediator)
            {
                _orNisanRepository = orNisanRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OrNisan>>> Handle(GetOrNisansQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OrNisan>>(await _orNisanRepository.GetListAsync());
            }
        }
    }
}