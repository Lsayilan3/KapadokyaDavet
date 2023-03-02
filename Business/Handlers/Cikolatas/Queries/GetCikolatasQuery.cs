
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

namespace Business.Handlers.Cikolatas.Queries
{

    public class GetCikolatasQuery : IRequest<IDataResult<IEnumerable<Cikolata>>>
    {
        public class GetCikolatasQueryHandler : IRequestHandler<GetCikolatasQuery, IDataResult<IEnumerable<Cikolata>>>
        {
            private readonly ICikolataRepository _cikolataRepository;
            private readonly IMediator _mediator;

            public GetCikolatasQueryHandler(ICikolataRepository cikolataRepository, IMediator mediator)
            {
                _cikolataRepository = cikolataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Cikolata>>> Handle(GetCikolatasQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Cikolata>>(await _cikolataRepository.GetListAsync());
            }
        }
    }
}