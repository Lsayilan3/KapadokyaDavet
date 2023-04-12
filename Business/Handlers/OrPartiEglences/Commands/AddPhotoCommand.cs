
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
using Business.Handlers.OrCofves.Queries;
using Business.Handlers.OrDuguns.Queries;
using Business.Handlers.OrEkipmans.Queries;
using Business.Handlers.Organizasyons.Queries;
using Business.Handlers.OrKinaas.Queries;
using Business.Handlers.OrKokteyls.Queries;
using Business.Handlers.OrNisans.Queries;
using Business.Handlers.OrPartiEglences.Queries;

namespace Business.Handlers.OrPartiEglences.Commands
{


    public class AddPhotoCommad : IRequest<IResult>
    {
        public int OrPartiEglenceId { get; set; }

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
                var result = await _mediator.Send(new GetOrPartiEglenceQuery { OrPartiEglenceId = request.OrPartiEglenceId });
                if (request.File.Length > 0)
                {
                    string folderPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/partyfun");

                    if(!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    string filePath = Path.Combine(folderPath, request.File.FileName);

                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.File.CopyToAsync(fileStream);
                    }
                    result.Data.Photo = "/uploads/partyfun/" + request.File.FileName;
                    /*myClass.Photo = "/uploads/" + file.FileName; */
                    var upResult = await _mediator.Send(new UpdateOrPartiEglenceCommand()
                    {
                        OrPartiEglenceId = result.Data.OrPartiEglenceId,
                        Detay = result.Data.Detay,
                        Photo = result.Data.Photo,

                    });
                }
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

