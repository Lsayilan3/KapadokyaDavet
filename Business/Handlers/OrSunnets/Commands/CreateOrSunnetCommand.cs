
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
using Business.Handlers.OrSunnets.ValidationRules;

namespace Business.Handlers.OrSunnets.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrSunnetCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Detay { get; set; }


        public class CreateOrSunnetCommandHandler : IRequestHandler<CreateOrSunnetCommand, IResult>
        {
            private readonly IOrSunnetRepository _orSunnetRepository;
            private readonly IMediator _mediator;
            public CreateOrSunnetCommandHandler(IOrSunnetRepository orSunnetRepository, IMediator mediator)
            {
                _orSunnetRepository = orSunnetRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrSunnetValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrSunnetCommand request, CancellationToken cancellationToken)
            {
                //var isThereOrSunnetRecord = _orSunnetRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereOrSunnetRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrSunnet = new OrSunnet
                {
                    Photo = request.Photo,
                    Detay = request.Detay,

                };

                _orSunnetRepository.Add(addedOrSunnet);
                await _orSunnetRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}