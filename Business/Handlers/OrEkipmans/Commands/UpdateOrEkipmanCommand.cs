
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
using Business.Handlers.OrEkipmans.ValidationRules;


namespace Business.Handlers.OrEkipmans.Commands
{


    public class UpdateOrEkipmanCommand : IRequest<IResult>
    {
        public int OrEkipmanId { get; set; }
        public string Photo { get; set; }
        public string Detay { get; set; }

        public class UpdateOrEkipmanCommandHandler : IRequestHandler<UpdateOrEkipmanCommand, IResult>
        {
            private readonly IOrEkipmanRepository _orEkipmanRepository;
            private readonly IMediator _mediator;

            public UpdateOrEkipmanCommandHandler(IOrEkipmanRepository orEkipmanRepository, IMediator mediator)
            {
                _orEkipmanRepository = orEkipmanRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrEkipmanValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrEkipmanCommand request, CancellationToken cancellationToken)
            {
                var isThereOrEkipmanRecord = await _orEkipmanRepository.GetAsync(u => u.OrEkipmanId == request.OrEkipmanId);


                isThereOrEkipmanRecord.Photo = request.Photo;
                isThereOrEkipmanRecord.Detay = request.Detay;


                _orEkipmanRepository.Update(isThereOrEkipmanRecord);
                await _orEkipmanRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

