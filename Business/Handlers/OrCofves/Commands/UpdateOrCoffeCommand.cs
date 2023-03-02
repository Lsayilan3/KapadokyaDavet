
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
using Business.Handlers.OrCofves.ValidationRules;


namespace Business.Handlers.OrCofves.Commands
{


    public class UpdateOrCoffeCommand : IRequest<IResult>
    {
        public int OrCoffeId { get; set; }
        public string Photo { get; set; }
        public string Detay { get; set; }

        public class UpdateOrCoffeCommandHandler : IRequestHandler<UpdateOrCoffeCommand, IResult>
        {
            private readonly IOrCoffeRepository _orCoffeRepository;
            private readonly IMediator _mediator;

            public UpdateOrCoffeCommandHandler(IOrCoffeRepository orCoffeRepository, IMediator mediator)
            {
                _orCoffeRepository = orCoffeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrCoffeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrCoffeCommand request, CancellationToken cancellationToken)
            {
                var isThereOrCoffeRecord = await _orCoffeRepository.GetAsync(u => u.OrCoffeId == request.OrCoffeId);


                isThereOrCoffeRecord.Photo = request.Photo;
                isThereOrCoffeRecord.Detay = request.Detay;


                _orCoffeRepository.Update(isThereOrCoffeRecord);
                await _orCoffeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

