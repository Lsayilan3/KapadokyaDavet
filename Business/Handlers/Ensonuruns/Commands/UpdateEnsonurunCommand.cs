
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
using Business.Handlers.Ensonuruns.ValidationRules;


namespace Business.Handlers.Ensonuruns.Commands
{


    public class UpdateEnsonurunCommand : IRequest<IResult>
    {
        public int EnsonurunId { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public class UpdateEnsonurunCommandHandler : IRequestHandler<UpdateEnsonurunCommand, IResult>
        {
            private readonly IEnsonurunRepository _ensonurunRepository;
            private readonly IMediator _mediator;

            public UpdateEnsonurunCommandHandler(IEnsonurunRepository ensonurunRepository, IMediator mediator)
            {
                _ensonurunRepository = ensonurunRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateEnsonurunValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateEnsonurunCommand request, CancellationToken cancellationToken)
            {
                var isThereEnsonurunRecord = await _ensonurunRepository.GetAsync(u => u.EnsonurunId == request.EnsonurunId);


                isThereEnsonurunRecord.Photo = request.Photo;
                isThereEnsonurunRecord.Title = request.Title;
                isThereEnsonurunRecord.Url = request.Url;


                _ensonurunRepository.Update(isThereEnsonurunRecord);
                await _ensonurunRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

