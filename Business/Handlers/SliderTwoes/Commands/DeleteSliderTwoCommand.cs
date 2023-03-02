
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


namespace Business.Handlers.SliderTwoes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteSliderTwoCommand : IRequest<IResult>
    {
        public int SliderTwoId { get; set; }

        public class DeleteSliderTwoCommandHandler : IRequestHandler<DeleteSliderTwoCommand, IResult>
        {
            private readonly ISliderTwoRepository _sliderTwoRepository;
            private readonly IMediator _mediator;

            public DeleteSliderTwoCommandHandler(ISliderTwoRepository sliderTwoRepository, IMediator mediator)
            {
                _sliderTwoRepository = sliderTwoRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteSliderTwoCommand request, CancellationToken cancellationToken)
            {
                var sliderTwoToDelete = _sliderTwoRepository.Get(p => p.SliderTwoId == request.SliderTwoId);

                _sliderTwoRepository.Delete(sliderTwoToDelete);
                await _sliderTwoRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

