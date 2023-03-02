
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

namespace Business.Handlers.OrKokteyls.Queries
{

    public class GetOrKokteylsQuery : IRequest<IDataResult<IEnumerable<OrKokteyl>>>
    {
        public class GetOrKokteylsQueryHandler : IRequestHandler<GetOrKokteylsQuery, IDataResult<IEnumerable<OrKokteyl>>>
        {
            private readonly IOrKokteylRepository _orKokteylRepository;
            private readonly IMediator _mediator;

            public GetOrKokteylsQueryHandler(IOrKokteylRepository orKokteylRepository, IMediator mediator)
            {
                _orKokteylRepository = orKokteylRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OrKokteyl>>> Handle(GetOrKokteylsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OrKokteyl>>(await _orKokteylRepository.GetListAsync());
            }
        }
    }
}