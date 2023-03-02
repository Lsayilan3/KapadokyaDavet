
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

namespace Business.Handlers.Organizasyons.Queries
{

    public class GetOrganizasyonsQuery : IRequest<IDataResult<IEnumerable<Organizasyon>>>
    {
        public class GetOrganizasyonsQueryHandler : IRequestHandler<GetOrganizasyonsQuery, IDataResult<IEnumerable<Organizasyon>>>
        {
            private readonly IOrganizasyonRepository _organizasyonRepository;
            private readonly IMediator _mediator;

            public GetOrganizasyonsQueryHandler(IOrganizasyonRepository organizasyonRepository, IMediator mediator)
            {
                _organizasyonRepository = organizasyonRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Organizasyon>>> Handle(GetOrganizasyonsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Organizasyon>>(await _organizasyonRepository.GetListAsync());
            }
        }
    }
}