﻿
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

namespace Business.Handlers.OrSunnets.Queries
{

    public class GetOrSunnetsQuery : IRequest<IDataResult<IEnumerable<OrSunnet>>>
    {
        public class GetOrSunnetsQueryHandler : IRequestHandler<GetOrSunnetsQuery, IDataResult<IEnumerable<OrSunnet>>>
        {
            private readonly IOrSunnetRepository _orSunnetRepository;
            private readonly IMediator _mediator;

            public GetOrSunnetsQueryHandler(IOrSunnetRepository orSunnetRepository, IMediator mediator)
            {
                _orSunnetRepository = orSunnetRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OrSunnet>>> Handle(GetOrSunnetsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OrSunnet>>(await _orSunnetRepository.GetListAsync());
            }
        }
    }
}