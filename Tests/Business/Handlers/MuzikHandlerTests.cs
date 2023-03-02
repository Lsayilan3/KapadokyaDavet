
using Business.Handlers.Muziks.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Muziks.Queries.GetMuzikQuery;
using Entities.Concrete;
using static Business.Handlers.Muziks.Queries.GetMuziksQuery;
using static Business.Handlers.Muziks.Commands.CreateMuzikCommand;
using Business.Handlers.Muziks.Commands;
using Business.Constants;
using static Business.Handlers.Muziks.Commands.UpdateMuzikCommand;
using static Business.Handlers.Muziks.Commands.DeleteMuzikCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class MuzikHandlerTests
    {
        Mock<IMuzikRepository> _muzikRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _muzikRepository = new Mock<IMuzikRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Muzik_GetQuery_Success()
        {
            //Arrange
            var query = new GetMuzikQuery();

            _muzikRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Muzik, bool>>>())).ReturnsAsync(new Muzik()
//propertyler buraya yazılacak
//{																		
//MuzikId = 1,
//MuzikName = "Test"
//}
);

            var handler = new GetMuzikQueryHandler(_muzikRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.MuzikId.Should().Be(1);

        }

        [Test]
        public async Task Muzik_GetQueries_Success()
        {
            //Arrange
            var query = new GetMuziksQuery();

            _muzikRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Muzik, bool>>>()))
                        .ReturnsAsync(new List<Muzik> { new Muzik() { /*TODO:propertyler buraya yazılacak MuzikId = 1, MuzikName = "test"*/ } });

            var handler = new GetMuziksQueryHandler(_muzikRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Muzik>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Muzik_CreateCommand_Success()
        {
            Muzik rt = null;
            //Arrange
            var command = new CreateMuzikCommand();
            //propertyler buraya yazılacak
            //command.MuzikName = "deneme";

            _muzikRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Muzik, bool>>>()))
                        .ReturnsAsync(rt);

            _muzikRepository.Setup(x => x.Add(It.IsAny<Muzik>())).Returns(new Muzik());

            var handler = new CreateMuzikCommandHandler(_muzikRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _muzikRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Muzik_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateMuzikCommand();
            //propertyler buraya yazılacak 
            //command.MuzikName = "test";

            _muzikRepository.Setup(x => x.Query())
                                           .Returns(new List<Muzik> { new Muzik() { /*TODO:propertyler buraya yazılacak MuzikId = 1, MuzikName = "test"*/ } }.AsQueryable());

            _muzikRepository.Setup(x => x.Add(It.IsAny<Muzik>())).Returns(new Muzik());

            var handler = new CreateMuzikCommandHandler(_muzikRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Muzik_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateMuzikCommand();
            //command.MuzikName = "test";

            _muzikRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Muzik, bool>>>()))
                        .ReturnsAsync(new Muzik() { /*TODO:propertyler buraya yazılacak MuzikId = 1, MuzikName = "deneme"*/ });

            _muzikRepository.Setup(x => x.Update(It.IsAny<Muzik>())).Returns(new Muzik());

            var handler = new UpdateMuzikCommandHandler(_muzikRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _muzikRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Muzik_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteMuzikCommand();

            _muzikRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Muzik, bool>>>()))
                        .ReturnsAsync(new Muzik() { /*TODO:propertyler buraya yazılacak MuzikId = 1, MuzikName = "deneme"*/});

            _muzikRepository.Setup(x => x.Delete(It.IsAny<Muzik>()));

            var handler = new DeleteMuzikCommandHandler(_muzikRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _muzikRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

