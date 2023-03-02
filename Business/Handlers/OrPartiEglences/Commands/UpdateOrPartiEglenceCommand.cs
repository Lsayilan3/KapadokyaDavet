
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.OrPartiEglences.ValidationRules;


namespace Business.Handlers.OrPartiEglences.Commands
{


    public class UpdateOrPartiEglenceCommand : IRequest<IResult>
    {
        public int OrPartiEglenceId { get; set; }
        public string Photo { get; set; }
        public string Detay { get; set; }

        public class UpdateOrPartiEglenceCommandHandler : IRequestHandler<UpdateOrPartiEglenceCommand, IResult>
        {
            private readonly IOrPartiEglenceRepository _orPartiEglenceRepository;
            private readonly IMediator _mediator;

            public UpdateOrPartiEglenceCommandHandler(IOrPartiEglenceRepository orPartiEglenceRepository, IMediator mediator)
            {
                _orPartiEglenceRepository = orPartiEglenceRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrPartiEglenceValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrPartiEglenceCommand request, CancellationToken cancellationToken)
            {
                var isThereOrPartiEglenceRecord = await _orPartiEglenceRepository.GetAsync(u => u.OrPartiEglenceId == request.OrPartiEglenceId);


                isThereOrPartiEglenceRecord.Photo = request.Photo;
                isThereOrPartiEglenceRecord.Detay = request.Detay;


                _orPartiEglenceRepository.Update(isThereOrPartiEglenceRecord);
                await _orPartiEglenceRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

