
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

namespace Business.Handlers.OrSokakLezzetis.Queries
{

    public class GetOrSokakLezzetisQuery : IRequest<IDataResult<IEnumerable<OrSokakLezzeti>>>
    {
        public class GetOrSokakLezzetisQueryHandler : IRequestHandler<GetOrSokakLezzetisQuery, IDataResult<IEnumerable<OrSokakLezzeti>>>
        {
            private readonly IOrSokakLezzetiRepository _orSokakLezzetiRepository;
            private readonly IMediator _mediator;

            public GetOrSokakLezzetisQueryHandler(IOrSokakLezzetiRepository orSokakLezzetiRepository, IMediator mediator)
            {
                _orSokakLezzetiRepository = orSokakLezzetiRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OrSokakLezzeti>>> Handle(GetOrSokakLezzetisQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OrSokakLezzeti>>(await _orSokakLezzetiRepository.GetListAsync());
            }
        }
    }
}