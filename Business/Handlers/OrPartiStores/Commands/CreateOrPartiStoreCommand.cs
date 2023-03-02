
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
using Business.Handlers.OrPartiStores.ValidationRules;

namespace Business.Handlers.OrPartiStores.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrPartiStoreCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Detay { get; set; }


        public class CreateOrPartiStoreCommandHandler : IRequestHandler<CreateOrPartiStoreCommand, IResult>
        {
            private readonly IOrPartiStoreRepository _orPartiStoreRepository;
            private readonly IMediator _mediator;
            public CreateOrPartiStoreCommandHandler(IOrPartiStoreRepository orPartiStoreRepository, IMediator mediator)
            {
                _orPartiStoreRepository = orPartiStoreRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrPartiStoreValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrPartiStoreCommand request, CancellationToken cancellationToken)
            {
                //var isThereOrPartiStoreRecord = _orPartiStoreRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereOrPartiStoreRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrPartiStore = new OrPartiStore
                {
                    Photo = request.Photo,
                    Detay = request.Detay,

                };

                _orPartiStoreRepository.Add(addedOrPartiStore);
                await _orPartiStoreRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}