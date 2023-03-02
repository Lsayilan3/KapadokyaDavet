
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
using Business.Handlers.Muziks.ValidationRules;


namespace Business.Handlers.Muziks.Commands
{


    public class UpdateMuzikCommand : IRequest<IResult>
    {
        public int MuzikId { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Detay { get; set; }

        public class UpdateMuzikCommandHandler : IRequestHandler<UpdateMuzikCommand, IResult>
        {
            private readonly IMuzikRepository _muzikRepository;
            private readonly IMediator _mediator;

            public UpdateMuzikCommandHandler(IMuzikRepository muzikRepository, IMediator mediator)
            {
                _muzikRepository = muzikRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateMuzikValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateMuzikCommand request, CancellationToken cancellationToken)
            {
                var isThereMuzikRecord = await _muzikRepository.GetAsync(u => u.MuzikId == request.MuzikId);


                isThereMuzikRecord.Photo = request.Photo;
                isThereMuzikRecord.Title = request.Title;
                isThereMuzikRecord.Tag = request.Tag;
                isThereMuzikRecord.Detay = request.Detay;


                _muzikRepository.Update(isThereMuzikRecord);
                await _muzikRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

