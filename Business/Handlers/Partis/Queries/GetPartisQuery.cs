
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

namespace Business.Handlers.Partis.Queries
{

    public class GetPartisQuery : IRequest<IDataResult<IEnumerable<Parti>>>
    {
        public class GetPartisQueryHandler : IRequestHandler<GetPartisQuery, IDataResult<IEnumerable<Parti>>>
        {
            private readonly IPartiRepository _partiRepository;
            private readonly IMediator _mediator;

            public GetPartisQueryHandler(IPartiRepository partiRepository, IMediator mediator)
            {
                _partiRepository = partiRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Parti>>> Handle(GetPartisQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Parti>>(await _partiRepository.GetListAsync());
            }
        }
    }
}