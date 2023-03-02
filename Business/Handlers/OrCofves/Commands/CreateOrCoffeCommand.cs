
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
using Business.Handlers.OrCofves.ValidationRules;

namespace Business.Handlers.OrCofves.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrCoffeCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Detay { get; set; }


        public class CreateOrCoffeCommandHandler : IRequestHandler<CreateOrCoffeCommand, IResult>
        {
            private readonly IOrCoffeRepository _orCoffeRepository;
            private readonly IMediator _mediator;
            public CreateOrCoffeCommandHandler(IOrCoffeRepository orCoffeRepository, IMediator mediator)
            {
                _orCoffeRepository = orCoffeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrCoffeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrCoffeCommand request, CancellationToken cancellationToken)
            {
                //var isThereOrCoffeRecord = _orCoffeRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereOrCoffeRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrCoffe = new OrCoffe
                {
                    Photo = request.Photo,
                    Detay = request.Detay,

                };

                _orCoffeRepository.Add(addedOrCoffe);
                await _orCoffeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}