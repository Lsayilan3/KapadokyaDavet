
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

namespace Business.Handlers.OrEkipmans.Queries
{

    public class GetOrEkipmansQuery : IRequest<IDataResult<IEnumerable<OrEkipman>>>
    {
        public class GetOrEkipmansQueryHandler : IRequestHandler<GetOrEkipmansQuery, IDataResult<IEnumerable<OrEkipman>>>
        {
            private readonly IOrEkipmanRepository _orEkipmanRepository;
            private readonly IMediator _mediator;

            public GetOrEkipmansQueryHandler(IOrEkipmanRepository orEkipmanRepository, IMediator mediator)
            {
                _orEkipmanRepository = orEkipmanRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OrEkipman>>> Handle(GetOrEkipmansQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OrEkipman>>(await _orEkipmanRepository.GetListAsync());
            }
        }
    }
}