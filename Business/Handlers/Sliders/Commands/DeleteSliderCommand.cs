
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Sliders.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteSliderCommand : IRequest<IResult>
    {
        public int SliderId { get; set; }

        public class DeleteSliderCommandHandler : IRequestHandler<DeleteSliderCommand, IResult>
        {
            private readonly ISliderRepository _sliderRepository;
            private readonly IMediator _mediator;

            public DeleteSliderCommandHandler(ISliderRepository sliderRepository, IMediator mediator)
            {
                _sliderRepository = sliderRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteSliderCommand request, CancellationToken cancellationToken)
            {
                var sliderToDelete = _sliderRepository.Get(p => p.SliderId == request.SliderId);

                _sliderRepository.Delete(sliderToDelete);
                await _sliderRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

