
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

namespace Business.Handlers.Lazers.Queries
{

    public class GetLazersQuery : IRequest<IDataResult<IEnumerable<Lazer>>>
    {
        public class GetLazersQueryHandler : IRequestHandler<GetLazersQuery, IDataResult<IEnumerable<Lazer>>>
        {
            private readonly ILazerRepository _lazerRepository;
            private readonly IMediator _mediator;

            public GetLazersQueryHandler(ILazerRepository lazerRepository, IMediator mediator)
            {
                _lazerRepository = lazerRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Lazer>>> Handle(GetLazersQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Lazer>>(await _lazerRepository.GetListAsync());
            }
        }
    }
}