
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
using Business.Handlers.OrKokteyls.ValidationRules;

namespace Business.Handlers.OrKokteyls.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrKokteylCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Detay { get; set; }


        public class CreateOrKokteylCommandHandler : IRequestHandler<CreateOrKokteylCommand, IResult>
        {
            private readonly IOrKokteylRepository _orKokteylRepository;
            private readonly IMediator _mediator;
            public CreateOrKokteylCommandHandler(IOrKokteylRepository orKokteylRepository, IMediator mediator)
            {
                _orKokteylRepository = orKokteylRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrKokteylValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrKokteylCommand request, CancellationToken cancellationToken)
            {
                //var isThereOrKokteylRecord = _orKokteylRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereOrKokteylRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrKokteyl = new OrKokteyl
                {
                    Photo = request.Photo,
                    Detay = request.Detay,

                };

                _orKokteylRepository.Add(addedOrKokteyl);
                await _orKokteylRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}