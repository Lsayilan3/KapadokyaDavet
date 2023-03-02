
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
using Business.Handlers.OrBabies.ValidationRules;

namespace Business.Handlers.OrBabies.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrBabyCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Detay { get; set; }


        public class CreateOrBabyCommandHandler : IRequestHandler<CreateOrBabyCommand, IResult>
        {
            private readonly IOrBabyRepository _orBabyRepository;
            private readonly IMediator _mediator;
            public CreateOrBabyCommandHandler(IOrBabyRepository orBabyRepository, IMediator mediator)
            {
                _orBabyRepository = orBabyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrBabyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrBabyCommand request, CancellationToken cancellationToken)
            {
                //var isThereOrBabyRecord = _orBabyRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereOrBabyRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrBaby = new OrBaby
                {
                    Photo = request.Photo,
                    Detay = request.Detay,

                };

                _orBabyRepository.Add(addedOrBaby);
                await _orBabyRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}