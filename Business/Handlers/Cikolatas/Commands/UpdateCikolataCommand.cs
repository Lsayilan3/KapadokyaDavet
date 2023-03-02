
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
using Business.Handlers.Cikolatas.ValidationRules;


namespace Business.Handlers.Cikolatas.Commands
{


    public class UpdateCikolataCommand : IRequest<IResult>
    {
        public int CikolataId { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Detay { get; set; }

        public class UpdateCikolataCommandHandler : IRequestHandler<UpdateCikolataCommand, IResult>
        {
            private readonly ICikolataRepository _cikolataRepository;
            private readonly IMediator _mediator;

            public UpdateCikolataCommandHandler(ICikolataRepository cikolataRepository, IMediator mediator)
            {
                _cikolataRepository = cikolataRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateCikolataValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateCikolataCommand request, CancellationToken cancellationToken)
            {
                var isThereCikolataRecord = await _cikolataRepository.GetAsync(u => u.CikolataId == request.CikolataId);


                isThereCikolataRecord.Photo = request.Photo;
                isThereCikolataRecord.Title = request.Title;
                isThereCikolataRecord.Tag = request.Tag;
                isThereCikolataRecord.Detay = request.Detay;


                _cikolataRepository.Update(isThereCikolataRecord);
                await _cikolataRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

