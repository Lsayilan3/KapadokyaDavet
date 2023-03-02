
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
using Business.Handlers.OrPikniks.ValidationRules;


namespace Business.Handlers.OrPikniks.Commands
{


    public class UpdateOrPiknikCommand : IRequest<IResult>
    {
        public int OrPiknikId { get; set; }
        public string Photo { get; set; }
        public string Detay { get; set; }

        public class UpdateOrPiknikCommandHandler : IRequestHandler<UpdateOrPiknikCommand, IResult>
        {
            private readonly IOrPiknikRepository _orPiknikRepository;
            private readonly IMediator _mediator;

            public UpdateOrPiknikCommandHandler(IOrPiknikRepository orPiknikRepository, IMediator mediator)
            {
                _orPiknikRepository = orPiknikRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrPiknikValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrPiknikCommand request, CancellationToken cancellationToken)
            {
                var isThereOrPiknikRecord = await _orPiknikRepository.GetAsync(u => u.OrPiknikId == request.OrPiknikId);


                isThereOrPiknikRecord.Photo = request.Photo;
                isThereOrPiknikRecord.Detay = request.Detay;


                _orPiknikRepository.Update(isThereOrPiknikRecord);
                await _orPiknikRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

