
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
using Business.Handlers.OrAcilises.ValidationRules;

namespace Business.Handlers.OrAcilises.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrAcilisCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Detay { get; set; }


        public class CreateOrAcilisCommandHandler : IRequestHandler<CreateOrAcilisCommand, IResult>
        {
            private readonly IOrAcilisRepository _orAcilisRepository;
            private readonly IMediator _mediator;
            public CreateOrAcilisCommandHandler(IOrAcilisRepository orAcilisRepository, IMediator mediator)
            {
                _orAcilisRepository = orAcilisRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrAcilisValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrAcilisCommand request, CancellationToken cancellationToken)
            {
                //var isThereOrAcilisRecord = _orAcilisRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereOrAcilisRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrAcilis = new OrAcilis
                {
                    Photo = request.Photo,
                    Detay = request.Detay,

                };

                _orAcilisRepository.Add(addedOrAcilis);
                await _orAcilisRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}