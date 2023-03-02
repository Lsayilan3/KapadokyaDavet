
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OrPersonelTeminis.Queries
{
    public class GetOrPersonelTeminiQuery : IRequest<IDataResult<OrPersonelTemini>>
    {
        public int OrPersonelTeminiId { get; set; }

        public class GetOrPersonelTeminiQueryHandler : IRequestHandler<GetOrPersonelTeminiQuery, IDataResult<OrPersonelTemini>>
        {
            private readonly IOrPersonelTeminiRepository _orPersonelTeminiRepository;
            private readonly IMediator _mediator;

            public GetOrPersonelTeminiQueryHandler(IOrPersonelTeminiRepository orPersonelTeminiRepository, IMediator mediator)
            {
                _orPersonelTeminiRepository = orPersonelTeminiRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OrPersonelTemini>> Handle(GetOrPersonelTeminiQuery request, CancellationToken cancellationToken)
            {
                var orPersonelTemini = await _orPersonelTeminiRepository.GetAsync(p => p.OrPersonelTeminiId == request.OrPersonelTeminiId);
                return new SuccessDataResult<OrPersonelTemini>(orPersonelTemini);
            }
        }
    }
}
