
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
using Business.Handlers.OrPersonelTeminis.ValidationRules;

namespace Business.Handlers.OrPersonelTeminis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrPersonelTeminiCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Detay { get; set; }


        public class CreateOrPersonelTeminiCommandHandler : IRequestHandler<CreateOrPersonelTeminiCommand, IResult>
        {
            private readonly IOrPersonelTeminiRepository _orPersonelTeminiRepository;
            private readonly IMediator _mediator;
            public CreateOrPersonelTeminiCommandHandler(IOrPersonelTeminiRepository orPersonelTeminiRepository, IMediator mediator)
            {
                _orPersonelTeminiRepository = orPersonelTeminiRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrPersonelTeminiValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrPersonelTeminiCommand request, CancellationToken cancellationToken)
            {
                //var isThereOrPersonelTeminiRecord = _orPersonelTeminiRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereOrPersonelTeminiRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrPersonelTemini = new OrPersonelTemini
                {
                    Photo = request.Photo,
                    Detay = request.Detay,

                };

                _orPersonelTeminiRepository.Add(addedOrPersonelTemini);
                await _orPersonelTeminiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}