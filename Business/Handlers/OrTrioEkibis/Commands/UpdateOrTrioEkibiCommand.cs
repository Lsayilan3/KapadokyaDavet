
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
using Business.Handlers.OrTrioEkibis.ValidationRules;


namespace Business.Handlers.OrTrioEkibis.Commands
{


    public class UpdateOrTrioEkibiCommand : IRequest<IResult>
    {
        public int OrTrioEkibiId { get; set; }
        public string Photo { get; set; }
        public string Detay { get; set; }

        public class UpdateOrTrioEkibiCommandHandler : IRequestHandler<UpdateOrTrioEkibiCommand, IResult>
        {
            private readonly IOrTrioEkibiRepository _orTrioEkibiRepository;
            private readonly IMediator _mediator;

            public UpdateOrTrioEkibiCommandHandler(IOrTrioEkibiRepository orTrioEkibiRepository, IMediator mediator)
            {
                _orTrioEkibiRepository = orTrioEkibiRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrTrioEkibiValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrTrioEkibiCommand request, CancellationToken cancellationToken)
            {
                var isThereOrTrioEkibiRecord = await _orTrioEkibiRepository.GetAsync(u => u.OrTrioEkibiId == request.OrTrioEkibiId);


                isThereOrTrioEkibiRecord.Photo = request.Photo;
                isThereOrTrioEkibiRecord.Detay = request.Detay;


                _orTrioEkibiRepository.Update(isThereOrTrioEkibiRecord);
                await _orTrioEkibiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

