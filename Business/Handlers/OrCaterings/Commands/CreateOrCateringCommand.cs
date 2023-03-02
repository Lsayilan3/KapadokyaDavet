
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
using Business.Handlers.OrCaterings.ValidationRules;

namespace Business.Handlers.OrCaterings.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrCateringCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Detay { get; set; }


        public class CreateOrCateringCommandHandler : IRequestHandler<CreateOrCateringCommand, IResult>
        {
            private readonly IOrCateringRepository _orCateringRepository;
            private readonly IMediator _mediator;
            public CreateOrCateringCommandHandler(IOrCateringRepository orCateringRepository, IMediator mediator)
            {
                _orCateringRepository = orCateringRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrCateringValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrCateringCommand request, CancellationToken cancellationToken)
            {
                //var isThereOrCateringRecord = _orCateringRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereOrCateringRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrCatering = new OrCatering
                {
                    Photo = request.Photo,
                    Detay = request.Detay,

                };

                _orCateringRepository.Add(addedOrCatering);
                await _orCateringRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}