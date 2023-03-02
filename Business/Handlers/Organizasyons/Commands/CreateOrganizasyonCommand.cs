
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Organizasyons.ValidationRules;

namespace Business.Handlers.Organizasyons.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrganizasyonCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Detay { get; set; }


        public class CreateOrganizasyonCommandHandler : IRequestHandler<CreateOrganizasyonCommand, IResult>
        {
            private readonly IOrganizasyonRepository _organizasyonRepository;
            private readonly IMediator _mediator;
            public CreateOrganizasyonCommandHandler(IOrganizasyonRepository organizasyonRepository, IMediator mediator)
            {
                _organizasyonRepository = organizasyonRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrganizasyonValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrganizasyonCommand request, CancellationToken cancellationToken)
            {
                //var isThereOrganizasyonRecord = _organizasyonRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereOrganizasyonRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrganizasyon = new Organizasyon
                {
                    Photo = request.Photo,
                    Title = request.Title,
                    Tag = request.Tag,
                    Detay = request.Detay,

                };

                _organizasyonRepository.Add(addedOrganizasyon);
                await _organizasyonRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}