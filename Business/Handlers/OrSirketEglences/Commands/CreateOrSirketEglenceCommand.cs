
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
using Business.Handlers.OrSirketEglences.ValidationRules;

namespace Business.Handlers.OrSirketEglences.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrSirketEglenceCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Detay { get; set; }


        public class CreateOrSirketEglenceCommandHandler : IRequestHandler<CreateOrSirketEglenceCommand, IResult>
        {
            private readonly IOrSirketEglenceRepository _orSirketEglenceRepository;
            private readonly IMediator _mediator;
            public CreateOrSirketEglenceCommandHandler(IOrSirketEglenceRepository orSirketEglenceRepository, IMediator mediator)
            {
                _orSirketEglenceRepository = orSirketEglenceRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrSirketEglenceValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrSirketEglenceCommand request, CancellationToken cancellationToken)
            {
                //var isThereOrSirketEglenceRecord = _orSirketEglenceRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereOrSirketEglenceRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrSirketEglence = new OrSirketEglence
                {
                    Photo = request.Photo,
                    Detay = request.Detay,

                };

                _orSirketEglenceRepository.Add(addedOrSirketEglence);
                await _orSirketEglenceRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}