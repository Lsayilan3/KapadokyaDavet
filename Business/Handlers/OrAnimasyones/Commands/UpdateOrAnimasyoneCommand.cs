
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
using Business.Handlers.OrAnimasyones.ValidationRules;


namespace Business.Handlers.OrAnimasyones.Commands
{


    public class UpdateOrAnimasyoneCommand : IRequest<IResult>
    {
        public int OrAnimasyoneId { get; set; }
        public string Photo { get; set; }
        public string Detay { get; set; }

        public class UpdateOrAnimasyoneCommandHandler : IRequestHandler<UpdateOrAnimasyoneCommand, IResult>
        {
            private readonly IOrAnimasyoneRepository _orAnimasyoneRepository;
            private readonly IMediator _mediator;

            public UpdateOrAnimasyoneCommandHandler(IOrAnimasyoneRepository orAnimasyoneRepository, IMediator mediator)
            {
                _orAnimasyoneRepository = orAnimasyoneRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrAnimasyoneValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrAnimasyoneCommand request, CancellationToken cancellationToken)
            {
                var isThereOrAnimasyoneRecord = await _orAnimasyoneRepository.GetAsync(u => u.OrAnimasyoneId == request.OrAnimasyoneId);


                isThereOrAnimasyoneRecord.Photo = request.Photo;
                isThereOrAnimasyoneRecord.Detay = request.Detay;


                _orAnimasyoneRepository.Update(isThereOrAnimasyoneRecord);
                await _orAnimasyoneRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

