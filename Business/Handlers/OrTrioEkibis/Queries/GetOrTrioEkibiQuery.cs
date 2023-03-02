
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OrTrioEkibis.Queries
{
    public class GetOrTrioEkibiQuery : IRequest<IDataResult<OrTrioEkibi>>
    {
        public int OrTrioEkibiId { get; set; }

        public class GetOrTrioEkibiQueryHandler : IRequestHandler<GetOrTrioEkibiQuery, IDataResult<OrTrioEkibi>>
        {
            private readonly IOrTrioEkibiRepository _orTrioEkibiRepository;
            private readonly IMediator _mediator;

            public GetOrTrioEkibiQueryHandler(IOrTrioEkibiRepository orTrioEkibiRepository, IMediator mediator)
            {
                _orTrioEkibiRepository = orTrioEkibiRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OrTrioEkibi>> Handle(GetOrTrioEkibiQuery request, CancellationToken cancellationToken)
            {
                var orTrioEkibi = await _orTrioEkibiRepository.GetAsync(p => p.OrTrioEkibiId == request.OrTrioEkibiId);
                return new SuccessDataResult<OrTrioEkibi>(orTrioEkibi);
            }
        }
    }
}
