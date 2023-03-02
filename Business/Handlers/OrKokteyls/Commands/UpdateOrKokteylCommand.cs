
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
using Business.Handlers.OrKokteyls.ValidationRules;


namespace Business.Handlers.OrKokteyls.Commands
{


    public class UpdateOrKokteylCommand : IRequest<IResult>
    {
        public int OrKokteylId { get; set; }
        public string Photo { get; set; }
        public string Detay { get; set; }

        public class UpdateOrKokteylCommandHandler : IRequestHandler<UpdateOrKokteylCommand, IResult>
        {
            private readonly IOrKokteylRepository _orKokteylRepository;
            private readonly IMediator _mediator;

            public UpdateOrKokteylCommandHandler(IOrKokteylRepository orKokteylRepository, IMediator mediator)
            {
                _orKokteylRepository = orKokteylRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrKokteylValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrKokteylCommand request, CancellationToken cancellationToken)
            {
                var isThereOrKokteylRecord = await _orKokteylRepository.GetAsync(u => u.OrKokteylId == request.OrKokteylId);


                isThereOrKokteylRecord.Photo = request.Photo;
                isThereOrKokteylRecord.Detay = request.Detay;


                _orKokteylRepository.Update(isThereOrKokteylRecord);
                await _orKokteylRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

