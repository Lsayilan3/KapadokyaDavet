
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

namespace Business.Handlers.OrCofves.Queries
{

    public class GetOrCofvesQuery : IRequest<IDataResult<IEnumerable<OrCoffe>>>
    {
        public class GetOrCofvesQueryHandler : IRequestHandler<GetOrCofvesQuery, IDataResult<IEnumerable<OrCoffe>>>
        {
            private readonly IOrCoffeRepository _orCoffeRepository;
            private readonly IMediator _mediator;

            public GetOrCofvesQueryHandler(IOrCoffeRepository orCoffeRepository, IMediator mediator)
            {
                _orCoffeRepository = orCoffeRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OrCoffe>>> Handle(GetOrCofvesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OrCoffe>>(await _orCoffeRepository.GetListAsync());
            }
        }
    }
}