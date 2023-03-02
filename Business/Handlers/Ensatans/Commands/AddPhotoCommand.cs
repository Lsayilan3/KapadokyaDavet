
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

using Microsoft.AspNetCore.Http;
using System.IO;

using Microsoft.AspNetCore.Hosting;
using Business.Handlers.Cikolatas.Queries;
using Business.Handlers.Ensatans.Queries;

namespace Business.Handlers.Ensatans.Commands
{


    public class AddPhotoCommad : IRequest<IResult>
    {
        public int EnsatanId { get; set; }

        public IFormFile File { get; set; }

        public class AddPhotoCommadHandler : IRequestHandler<AddPhotoCommad, IResult>
        {
            private readonly IMediator _mediator;
            IHostingEnvironment _hostingEnvironment;
            public AddPhotoCommadHandler(IMediator mediator, IHostingEnvironment hostingEnvironment)
            {
                _mediator = mediator;
                _hostingEnvironment = hostingEnvironment;
            }


            //[SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(AddPhotoCommad request, CancellationToken cancellationToken)
            {
                var result = await _mediator.Send(new GetEnsatanQuery { EnsatanId = request.EnsatanId });
                if (request.File.Length > 0)
                {
                    string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/bestselling");
                    string filePath = Path.Combine(uploads, request.File.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.File.CopyToAsync(fileStream);
                    }
                    result.Data.Photo = "/uploads/bestselling/" + request.File.FileName;
                    /*myClass.Photo = "/uploads/" + file.FileName; */
                    var upResult = await _mediator.Send(new UpdateEnsatanCommand()
                    {
                        Title = result.Data.Title,
                        Url = result.Data.Url,
                        EnsatanId = result.Data.EnsatanId,           
                        Photo = result.Data.Photo,
                    });
                }
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

