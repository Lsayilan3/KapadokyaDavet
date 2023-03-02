
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
using Business.Handlers.OrAcilises.ValidationRules;


namespace Business.Handlers.OrAcilises.Commands
{


    public class UpdateOrAcilisCommand : IRequest<IResult>
    {
        public int OrAcilisId { get; set; }
        public string Photo { get; set; }
        public string Detay { get; set; }

        public class UpdateOrAcilisCommandHandler : IRequestHandler<UpdateOrAcilisCommand, IResult>
        {
            private readonly IOrAcilisRepository _orAcilisRepository;
            private readonly IMediator _mediator;

            public UpdateOrAcilisCommandHandler(IOrAcilisRepository orAcilisRepository, IMediator mediator)
            {
                _orAcilisRepository = orAcilisRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrAcilisValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrAcilisCommand request, CancellationToken cancellationToken)
            {
                var isThereOrAcilisRecord = await _orAcilisRepository.GetAsync(u => u.OrAcilisId == request.OrAcilisId);


                isThereOrAcilisRecord.Photo = request.Photo;
                isThereOrAcilisRecord.Detay = request.Detay;


                _orAcilisRepository.Update(isThereOrAcilisRecord);
                await _orAcilisRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

