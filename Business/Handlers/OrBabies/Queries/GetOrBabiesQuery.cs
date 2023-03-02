
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

namespace Business.Handlers.OrBabies.Queries
{

    public class GetOrBabiesQuery : IRequest<IDataResult<IEnumerable<OrBaby>>>
    {
        public class GetOrBabiesQueryHandler : IRequestHandler<GetOrBabiesQuery, IDataResult<IEnumerable<OrBaby>>>
        {
            private readonly IOrBabyRepository _orBabyRepository;
            private readonly IMediator _mediator;

            public GetOrBabiesQueryHandler(IOrBabyRepository orBabyRepository, IMediator mediator)
            {
                _orBabyRepository = orBabyRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OrBaby>>> Handle(GetOrBabiesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OrBaby>>(await _orBabyRepository.GetListAsync());
            }
        }
    }
}