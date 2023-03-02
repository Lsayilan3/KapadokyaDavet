
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.OrPartiEglences.ValidationRules;

namespace Business.Handlers.OrPartiEglences.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrPartiEglenceCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Detay { get; set; }


        public class CreateOrPartiEglenceCommandHandler : IRequestHandler<CreateOrPartiEglenceCommand, IResult>
        {
            private readonly IOrPartiEglenceRepository _orPartiEglenceRepository;
            private readonly IMediator _mediator;
            public CreateOrPartiEglenceCommandHandler(IOrPartiEglenceRepository orPartiEglenceRepository, IMediator mediator)
            {
                _orPartiEglenceRepository = orPartiEglenceRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrPartiEglenceValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrPartiEglenceCommand request, CancellationToken cancellationToken)
            {
                //var isThereOrPartiEglenceRecord = _orPartiEglenceRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereOrPartiEglenceRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrPartiEglence = new OrPartiEglence
                {
                    Photo = request.Photo,
                    Detay = request.Detay,

                };

                _orPartiEglenceRepository.Add(addedOrPartiEglence);
                await _orPartiEglenceRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}