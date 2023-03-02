
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
using Business.Handlers.Ensatans.ValidationRules;


namespace Business.Handlers.Ensatans.Commands
{


    public class UpdateEnsatanCommand : IRequest<IResult>
    {
        public int EnsatanId { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public class UpdateEnsatanCommandHandler : IRequestHandler<UpdateEnsatanCommand, IResult>
        {
            private readonly IEnsatanRepository _ensatanRepository;
            private readonly IMediator _mediator;

            public UpdateEnsatanCommandHandler(IEnsatanRepository ensatanRepository, IMediator mediator)
            {
                _ensatanRepository = ensatanRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateEnsatanValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateEnsatanCommand request, CancellationToken cancellationToken)
            {
                var isThereEnsatanRecord = await _ensatanRepository.GetAsync(u => u.EnsatanId == request.EnsatanId);


                isThereEnsatanRecord.Photo = request.Photo;
                isThereEnsatanRecord.Title = request.Title;
                isThereEnsatanRecord.Url = request.Url;


                _ensatanRepository.Update(isThereEnsatanRecord);
                await _ensatanRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

