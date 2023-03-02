
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OrBabies.Queries
{
    public class GetOrBabyQuery : IRequest<IDataResult<OrBaby>>
    {
        public int OrBabyId { get; set; }

        public class GetOrBabyQueryHandler : IRequestHandler<GetOrBabyQuery, IDataResult<OrBaby>>
        {
            private readonly IOrBabyRepository _orBabyRepository;
            private readonly IMediator _mediator;

            public GetOrBabyQueryHandler(IOrBabyRepository orBabyRepository, IMediator mediator)
            {
                _orBabyRepository = orBabyRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OrBaby>> Handle(GetOrBabyQuery request, CancellationToken cancellationToken)
            {
                var orBaby = await _orBabyRepository.GetAsync(p => p.OrBabyId == request.OrBabyId);
                return new SuccessDataResult<OrBaby>(orBaby);
            }
        }
    }
}
