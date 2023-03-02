
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OrSokakLezzetis.Queries
{
    public class GetOrSokakLezzetiQuery : IRequest<IDataResult<OrSokakLezzeti>>
    {
        public int OrSokakLezzetiId { get; set; }

        public class GetOrSokakLezzetiQueryHandler : IRequestHandler<GetOrSokakLezzetiQuery, IDataResult<OrSokakLezzeti>>
        {
            private readonly IOrSokakLezzetiRepository _orSokakLezzetiRepository;
            private readonly IMediator _mediator;

            public GetOrSokakLezzetiQueryHandler(IOrSokakLezzetiRepository orSokakLezzetiRepository, IMediator mediator)
            {
                _orSokakLezzetiRepository = orSokakLezzetiRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OrSokakLezzeti>> Handle(GetOrSokakLezzetiQuery request, CancellationToken cancellationToken)
            {
                var orSokakLezzeti = await _orSokakLezzetiRepository.GetAsync(p => p.OrSokakLezzetiId == request.OrSokakLezzetiId);
                return new SuccessDataResult<OrSokakLezzeti>(orSokakLezzeti);
            }
        }
    }
}
