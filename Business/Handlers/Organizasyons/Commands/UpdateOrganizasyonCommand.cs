
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
using Business.Handlers.Organizasyons.ValidationRules;


namespace Business.Handlers.Organizasyons.Commands
{


    public class UpdateOrganizasyonCommand : IRequest<IResult>
    {
        public int OrganizasyonId { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Detay { get; set; }

        public class UpdateOrganizasyonCommandHandler : IRequestHandler<UpdateOrganizasyonCommand, IResult>
        {
            private readonly IOrganizasyonRepository _organizasyonRepository;
            private readonly IMediator _mediator;

            public UpdateOrganizasyonCommandHandler(IOrganizasyonRepository organizasyonRepository, IMediator mediator)
            {
                _organizasyonRepository = organizasyonRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrganizasyonValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrganizasyonCommand request, CancellationToken cancellationToken)
            {
                var isThereOrganizasyonRecord = await _organizasyonRepository.GetAsync(u => u.OrganizasyonId == request.OrganizasyonId);


                isThereOrganizasyonRecord.Photo = request.Photo;
                isThereOrganizasyonRecord.Title = request.Title;
                isThereOrganizasyonRecord.Tag = request.Tag;
                isThereOrganizasyonRecord.Detay = request.Detay;


                _organizasyonRepository.Update(isThereOrganizasyonRecord);
                await _organizasyonRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

