
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OrPikniks.Queries
{
    public class GetOrPiknikQuery : IRequest<IDataResult<OrPiknik>>
    {
        public int OrPiknikId { get; set; }

        public class GetOrPiknikQueryHandler : IRequestHandler<GetOrPiknikQuery, IDataResult<OrPiknik>>
        {
            private readonly IOrPiknikRepository _orPiknikRepository;
            private readonly IMediator _mediator;

            public GetOrPiknikQueryHandler(IOrPiknikRepository orPiknikRepository, IMediator mediator)
            {
                _orPiknikRepository = orPiknikRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OrPiknik>> Handle(GetOrPiknikQuery request, CancellationToken cancellationToken)
            {
                var orPiknik = await _orPiknikRepository.GetAsync(p => p.OrPiknikId == request.OrPiknikId);
                return new SuccessDataResult<OrPiknik>(orPiknik);
            }
        }
    }
}
