
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

namespace Business.Handlers.OrTrioEkibis.Queries
{

    public class GetOrTrioEkibisQuery : IRequest<IDataResult<IEnumerable<OrTrioEkibi>>>
    {
        public class GetOrTrioEkibisQueryHandler : IRequestHandler<GetOrTrioEkibisQuery, IDataResult<IEnumerable<OrTrioEkibi>>>
        {
            private readonly IOrTrioEkibiRepository _orTrioEkibiRepository;
            private readonly IMediator _mediator;

            public GetOrTrioEkibisQueryHandler(IOrTrioEkibiRepository orTrioEkibiRepository, IMediator mediator)
            {
                _orTrioEkibiRepository = orTrioEkibiRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OrTrioEkibi>>> Handle(GetOrTrioEkibisQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OrTrioEkibi>>(await _orTrioEkibiRepository.GetListAsync());
            }
        }
    }
}