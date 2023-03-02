
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
using Business.Handlers.OrSokakLezzetis.ValidationRules;


namespace Business.Handlers.OrSokakLezzetis.Commands
{


    public class UpdateOrSokakLezzetiCommand : IRequest<IResult>
    {
        public int OrSokakLezzetiId { get; set; }
        public string Photo { get; set; }
        public string Detay { get; set; }

        public class UpdateOrSokakLezzetiCommandHandler : IRequestHandler<UpdateOrSokakLezzetiCommand, IResult>
        {
            private readonly IOrSokakLezzetiRepository _orSokakLezzetiRepository;
            private readonly IMediator _mediator;

            public UpdateOrSokakLezzetiCommandHandler(IOrSokakLezzetiRepository orSokakLezzetiRepository, IMediator mediator)
            {
                _orSokakLezzetiRepository = orSokakLezzetiRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrSokakLezzetiValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrSokakLezzetiCommand request, CancellationToken cancellationToken)
            {
                var isThereOrSokakLezzetiRecord = await _orSokakLezzetiRepository.GetAsync(u => u.OrSokakLezzetiId == request.OrSokakLezzetiId);


                isThereOrSokakLezzetiRecord.Photo = request.Photo;
                isThereOrSokakLezzetiRecord.Detay = request.Detay;


                _orSokakLezzetiRepository.Update(isThereOrSokakLezzetiRecord);
                await _orSokakLezzetiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

