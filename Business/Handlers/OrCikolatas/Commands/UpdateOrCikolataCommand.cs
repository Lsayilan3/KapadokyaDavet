
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
using Business.Handlers.OrCikolatas.ValidationRules;


namespace Business.Handlers.OrCikolatas.Commands
{


    public class UpdateOrCikolataCommand : IRequest<IResult>
    {
        public int OrCikolataId { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Detay { get; set; }

        public class UpdateOrCikolataCommandHandler : IRequestHandler<UpdateOrCikolataCommand, IResult>
        {
            private readonly IOrCikolataRepository _orCikolataRepository;
            private readonly IMediator _mediator;

            public UpdateOrCikolataCommandHandler(IOrCikolataRepository orCikolataRepository, IMediator mediator)
            {
                _orCikolataRepository = orCikolataRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrCikolataValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrCikolataCommand request, CancellationToken cancellationToken)
            {
                var isThereOrCikolataRecord = await _orCikolataRepository.GetAsync(u => u.OrCikolataId == request.OrCikolataId);


                isThereOrCikolataRecord.Photo = request.Photo;
                isThereOrCikolataRecord.Title = request.Title;
                isThereOrCikolataRecord.Tag = request.Tag;
                isThereOrCikolataRecord.Detay = request.Detay;


                _orCikolataRepository.Update(isThereOrCikolataRecord);
                await _orCikolataRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

