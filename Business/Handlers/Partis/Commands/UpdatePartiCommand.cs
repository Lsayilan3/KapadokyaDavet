
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
using Business.Handlers.Partis.ValidationRules;


namespace Business.Handlers.Partis.Commands
{


    public class UpdatePartiCommand : IRequest<IResult>
    {
        public int PartiId { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Price { get; set; }
        public string DiscountPrice { get; set; }

        public class UpdatePartiCommandHandler : IRequestHandler<UpdatePartiCommand, IResult>
        {
            private readonly IPartiRepository _partiRepository;
            private readonly IMediator _mediator;

            public UpdatePartiCommandHandler(IPartiRepository partiRepository, IMediator mediator)
            {
                _partiRepository = partiRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdatePartiValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdatePartiCommand request, CancellationToken cancellationToken)
            {
                var isTherePartiRecord = await _partiRepository.GetAsync(u => u.PartiId == request.PartiId);


                isTherePartiRecord.Photo = request.Photo;
                isTherePartiRecord.Title = request.Title;
                isTherePartiRecord.Tag = request.Tag;
                isTherePartiRecord.Price = request.Price;
                isTherePartiRecord.DiscountPrice = request.DiscountPrice;


                _partiRepository.Update(isTherePartiRecord);
                await _partiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

