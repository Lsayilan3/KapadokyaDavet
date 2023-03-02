
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
using Business.Handlers.OrPartiStores.ValidationRules;


namespace Business.Handlers.OrPartiStores.Commands
{


    public class UpdateOrPartiStoreCommand : IRequest<IResult>
    {
        public int OrPartiStoreId { get; set; }
        public string Photo { get; set; }
        public string Detay { get; set; }

        public class UpdateOrPartiStoreCommandHandler : IRequestHandler<UpdateOrPartiStoreCommand, IResult>
        {
            private readonly IOrPartiStoreRepository _orPartiStoreRepository;
            private readonly IMediator _mediator;

            public UpdateOrPartiStoreCommandHandler(IOrPartiStoreRepository orPartiStoreRepository, IMediator mediator)
            {
                _orPartiStoreRepository = orPartiStoreRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrPartiStoreValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrPartiStoreCommand request, CancellationToken cancellationToken)
            {
                var isThereOrPartiStoreRecord = await _orPartiStoreRepository.GetAsync(u => u.OrPartiStoreId == request.OrPartiStoreId);


                isThereOrPartiStoreRecord.Photo = request.Photo;
                isThereOrPartiStoreRecord.Detay = request.Detay;


                _orPartiStoreRepository.Update(isThereOrPartiStoreRecord);
                await _orPartiStoreRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

