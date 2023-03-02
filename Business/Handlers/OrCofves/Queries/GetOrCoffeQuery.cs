
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OrCofves.Queries
{
    public class GetOrCoffeQuery : IRequest<IDataResult<OrCoffe>>
    {
        public int OrCoffeId { get; set; }

        public class GetOrCoffeQueryHandler : IRequestHandler<GetOrCoffeQuery, IDataResult<OrCoffe>>
        {
            private readonly IOrCoffeRepository _orCoffeRepository;
            private readonly IMediator _mediator;

            public GetOrCoffeQueryHandler(IOrCoffeRepository orCoffeRepository, IMediator mediator)
            {
                _orCoffeRepository = orCoffeRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OrCoffe>> Handle(GetOrCoffeQuery request, CancellationToken cancellationToken)
            {
                var orCoffe = await _orCoffeRepository.GetAsync(p => p.OrCoffeId == request.OrCoffeId);
                return new SuccessDataResult<OrCoffe>(orCoffe);
            }
        }
    }
}
