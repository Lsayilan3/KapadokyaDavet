
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Lazers.Queries
{
    public class GetLazerQuery : IRequest<IDataResult<Lazer>>
    {
        public int LazerId { get; set; }

        public class GetLazerQueryHandler : IRequestHandler<GetLazerQuery, IDataResult<Lazer>>
        {
            private readonly ILazerRepository _lazerRepository;
            private readonly IMediator _mediator;

            public GetLazerQueryHandler(ILazerRepository lazerRepository, IMediator mediator)
            {
                _lazerRepository = lazerRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Lazer>> Handle(GetLazerQuery request, CancellationToken cancellationToken)
            {
                var lazer = await _lazerRepository.GetAsync(p => p.LazerId == request.LazerId);
                return new SuccessDataResult<Lazer>(lazer);
            }
        }
    }
}
