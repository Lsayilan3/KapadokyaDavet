
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
using Business.Handlers.SpotCategoryies.ValidationRules;

namespace Business.Handlers.SpotCategoryies.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateSpotCategoryyCommand : IRequest<IResult>
    {

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }


        public class CreateSpotCategoryyCommandHandler : IRequestHandler<CreateSpotCategoryyCommand, IResult>
        {
            private readonly ISpotCategoryyRepository _spotCategoryyRepository;
            private readonly IMediator _mediator;
            public CreateSpotCategoryyCommandHandler(ISpotCategoryyRepository spotCategoryyRepository, IMediator mediator)
            {
                _spotCategoryyRepository = spotCategoryyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateSpotCategoryyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateSpotCategoryyCommand request, CancellationToken cancellationToken)
            {
                //var isThereSpotCategoryyRecord = _spotCategoryyRepository.Query().Any(u => u.CategoryId == request.CategoryId);

                //if (isThereSpotCategoryyRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedSpotCategoryy = new SpotCategoryy
                {
                    CategoryId = request.CategoryId,
                    CategoryName = request.CategoryName,

                };

                _spotCategoryyRepository.Add(addedSpotCategoryy);
                await _spotCategoryyRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}