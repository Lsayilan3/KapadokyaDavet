
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
using Business.Handlers.GalleryTwoes.ValidationRules;


namespace Business.Handlers.GalleryTwoes.Commands
{


    public class UpdateGalleryTwoCommand : IRequest<IResult>
    {
        public int GalleryTwoId { get; set; }
        public string Photo { get; set; }

        public class UpdateGalleryTwoCommandHandler : IRequestHandler<UpdateGalleryTwoCommand, IResult>
        {
            private readonly IGalleryTwoRepository _galleryTwoRepository;
            private readonly IMediator _mediator;

            public UpdateGalleryTwoCommandHandler(IGalleryTwoRepository galleryTwoRepository, IMediator mediator)
            {
                _galleryTwoRepository = galleryTwoRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateGalleryTwoValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateGalleryTwoCommand request, CancellationToken cancellationToken)
            {
                var isThereGalleryTwoRecord = await _galleryTwoRepository.GetAsync(u => u.GalleryTwoId == request.GalleryTwoId);


                isThereGalleryTwoRecord.Photo = request.Photo;


                _galleryTwoRepository.Update(isThereGalleryTwoRecord);
                await _galleryTwoRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

