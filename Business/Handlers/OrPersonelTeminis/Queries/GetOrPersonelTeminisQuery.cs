
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

namespace Business.Handlers.OrPersonelTeminis.Queries
{

    public class GetOrPersonelTeminisQuery : IRequest<IDataResult<IEnumerable<OrPersonelTemini>>>
    {
        public class GetOrPersonelTeminisQueryHandler : IRequestHandler<GetOrPersonelTeminisQuery, IDataResult<IEnumerable<OrPersonelTemini>>>
        {
            private readonly IOrPersonelTeminiRepository _orPersonelTeminiRepository;
            private readonly IMediator _mediator;

            public GetOrPersonelTeminisQueryHandler(IOrPersonelTeminiRepository orPersonelTeminiRepository, IMediator mediator)
            {
                _orPersonelTeminiRepository = orPersonelTeminiRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OrPersonelTemini>>> Handle(GetOrPersonelTeminisQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OrPersonelTemini>>(await _orPersonelTeminiRepository.GetListAsync());
            }
        }
    }
}