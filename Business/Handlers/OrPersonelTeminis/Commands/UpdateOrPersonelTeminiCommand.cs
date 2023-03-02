
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
using Business.Handlers.OrPersonelTeminis.ValidationRules;


namespace Business.Handlers.OrPersonelTeminis.Commands
{


    public class UpdateOrPersonelTeminiCommand : IRequest<IResult>
    {
        public int OrPersonelTeminiId { get; set; }
        public string Photo { get; set; }
        public string Detay { get; set; }

        public class UpdateOrPersonelTeminiCommandHandler : IRequestHandler<UpdateOrPersonelTeminiCommand, IResult>
        {
            private readonly IOrPersonelTeminiRepository _orPersonelTeminiRepository;
            private readonly IMediator _mediator;

            public UpdateOrPersonelTeminiCommandHandler(IOrPersonelTeminiRepository orPersonelTeminiRepository, IMediator mediator)
            {
                _orPersonelTeminiRepository = orPersonelTeminiRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrPersonelTeminiValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrPersonelTeminiCommand request, CancellationToken cancellationToken)
            {
                var isThereOrPersonelTeminiRecord = await _orPersonelTeminiRepository.GetAsync(u => u.OrPersonelTeminiId == request.OrPersonelTeminiId);


                isThereOrPersonelTeminiRecord.Photo = request.Photo;
                isThereOrPersonelTeminiRecord.Detay = request.Detay;


                _orPersonelTeminiRepository.Update(isThereOrPersonelTeminiRecord);
                await _orPersonelTeminiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

