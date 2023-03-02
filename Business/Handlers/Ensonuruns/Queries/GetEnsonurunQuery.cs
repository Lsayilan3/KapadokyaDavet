
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Ensonuruns.Queries
{
    public class GetEnsonurunQuery : IRequest<IDataResult<Ensonurun>>
    {
        public int EnsonurunId { get; set; }

        public class GetEnsonurunQueryHandler : IRequestHandler<GetEnsonurunQuery, IDataResult<Ensonurun>>
        {
            private readonly IEnsonurunRepository _ensonurunRepository;
            private readonly IMediator _mediator;

            public GetEnsonurunQueryHandler(IEnsonurunRepository ensonurunRepository, IMediator mediator)
            {
                _ensonurunRepository = ensonurunRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Ensonurun>> Handle(GetEnsonurunQuery request, CancellationToken cancellationToken)
            {
                var ensonurun = await _ensonurunRepository.GetAsync(p => p.EnsonurunId == request.EnsonurunId);
                return new SuccessDataResult<Ensonurun>(ensonurun);
            }
        }
    }
}
