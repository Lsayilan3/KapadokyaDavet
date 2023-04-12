
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

namespace Business.Handlers.OrPartiStores.Queries
{

    public class GetOrPartiStoresQuery : IRequest<IDataResult<IEnumerable<OrPartiStore>>>
    {
        public class GetOrPartiStoresQueryHandler : IRequestHandler<GetOrPartiStoresQuery, IDataResult<IEnumerable<OrPartiStore>>>
        {
            private readonly IOrPartiStoreRepository _orPartiStoreRepository;
            private readonly IMediator _mediator;

            public GetOrPartiStoresQueryHandler(IOrPartiStoreRepository orPartiStoreRepository, IMediator mediator)
            {
                _orPartiStoreRepository = orPartiStoreRepository;
                _mediator = mediator;
            }

           
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OrPartiStore>>> Handle(GetOrPartiStoresQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OrPartiStore>>(await _orPartiStoreRepository.GetListAsync());
            }
        }
    }
}