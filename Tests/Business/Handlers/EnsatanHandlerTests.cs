
using Business.Handlers.Ensatans.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Ensatans.Queries.GetEnsatanQuery;
using Entities.Concrete;
using static Business.Handlers.Ensatans.Queries.GetEnsatansQuery;
using static Business.Handlers.Ensatans.Commands.CreateEnsatanCommand;
using Business.Handlers.Ensatans.Commands;
using Business.Constants;
using static Business.Handlers.Ensatans.Commands.UpdateEnsatanCommand;
using static Business.Handlers.Ensatans.Commands.DeleteEnsatanCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class EnsatanHandlerTests
    {
        Mock<IEnsatanRepository> _ensatanRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _ensatanRepository = new Mock<IEnsatanRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Ensatan_GetQuery_Success()
        {
            //Arrange
            var query = new GetEnsatanQuery();

            _ensatanRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Ensatan, bool>>>())).ReturnsAsync(new Ensatan()
//propertyler buraya yazılacak
//{																		
//EnsatanId = 1,
//EnsatanName = "Test"
//}
);

            var handler = new GetEnsatanQueryHandler(_ensatanRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.EnsatanId.Should().Be(1);

        }

        [Test]
        public async Task Ensatan_GetQueries_Success()
        {
            //Arrange
            var query = new GetEnsatansQuery();

            _ensatanRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Ensatan, bool>>>()))
                        .ReturnsAsync(new List<Ensatan> { new Ensatan() { /*TODO:propertyler buraya yazılacak EnsatanId = 1, EnsatanName = "test"*/ } });

            var handler = new GetEnsatansQueryHandler(_ensatanRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Ensatan>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Ensatan_CreateCommand_Success()
        {
            Ensatan rt = null;
            //Arrange
            var command = new CreateEnsatanCommand();
            //propertyler buraya yazılacak
            //command.EnsatanName = "deneme";

            _ensatanRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Ensatan, bool>>>()))
                        .ReturnsAsync(rt);

            _ensatanRepository.Setup(x => x.Add(It.IsAny<Ensatan>())).Returns(new Ensatan());

            var handler = new CreateEnsatanCommandHandler(_ensatanRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _ensatanRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Ensatan_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateEnsatanCommand();
            //propertyler buraya yazılacak 
            //command.EnsatanName = "test";

            _ensatanRepository.Setup(x => x.Query())
                                           .Returns(new List<Ensatan> { new Ensatan() { /*TODO:propertyler buraya yazılacak EnsatanId = 1, EnsatanName = "test"*/ } }.AsQueryable());

            _ensatanRepository.Setup(x => x.Add(It.IsAny<Ensatan>())).Returns(new Ensatan());

            var handler = new CreateEnsatanCommandHandler(_ensatanRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Ensatan_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateEnsatanCommand();
            //command.EnsatanName = "test";

            _ensatanRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Ensatan, bool>>>()))
                        .ReturnsAsync(new Ensatan() { /*TODO:propertyler buraya yazılacak EnsatanId = 1, EnsatanName = "deneme"*/ });

            _ensatanRepository.Setup(x => x.Update(It.IsAny<Ensatan>())).Returns(new Ensatan());

            var handler = new UpdateEnsatanCommandHandler(_ensatanRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _ensatanRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Ensatan_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteEnsatanCommand();

            _ensatanRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Ensatan, bool>>>()))
                        .ReturnsAsync(new Ensatan() { /*TODO:propertyler buraya yazılacak EnsatanId = 1, EnsatanName = "deneme"*/});

            _ensatanRepository.Setup(x => x.Delete(It.IsAny<Ensatan>()));

            var handler = new DeleteEnsatanCommandHandler(_ensatanRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _ensatanRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

