
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
using Business.Handlers.OrNisans.ValidationRules;

namespace Business.Handlers.OrNisans.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrNisanCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Detay { get; set; }


        public class CreateOrNisanCommandHandler : IRequestHandler<CreateOrNisanCommand, IResult>
        {
            private readonly IOrNisanRepository _orNisanRepository;
            private readonly IMediator _mediator;
            public CreateOrNisanCommandHandler(IOrNisanRepository orNisanRepository, IMediator mediator)
            {
                _orNisanRepository = orNisanRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrNisanValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrNisanCommand request, CancellationToken cancellationToken)
            {
                //var isThereOrNisanRecord = _orNisanRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereOrNisanRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrNisan = new OrNisan
                {
                    Photo = request.Photo,
                    Detay = request.Detay,

                };

                _orNisanRepository.Add(addedOrNisan);
                await _orNisanRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}