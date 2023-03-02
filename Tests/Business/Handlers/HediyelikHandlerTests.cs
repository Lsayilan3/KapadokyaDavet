
using Business.Handlers.Hediyeliks.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Hediyeliks.Queries.GetHediyelikQuery;
using Entities.Concrete;
using static Business.Handlers.Hediyeliks.Queries.GetHediyeliksQuery;
using static Business.Handlers.Hediyeliks.Commands.CreateHediyelikCommand;
using Business.Handlers.Hediyeliks.Commands;
using Business.Constants;
using static Business.Handlers.Hediyeliks.Commands.UpdateHediyelikCommand;
using static Business.Handlers.Hediyeliks.Commands.DeleteHediyelikCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class HediyelikHandlerTests
    {
        Mock<IHediyelikRepository> _hediyelikRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _hediyelikRepository = new Mock<IHediyelikRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Hediyelik_GetQuery_Success()
        {
            //Arrange
            var query = new GetHediyelikQuery();

            _hediyelikRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Hediyelik, bool>>>())).ReturnsAsync(new Hediyelik()
//propertyler buraya yazılacak
//{																		
//HediyelikId = 1,
//HediyelikName = "Test"
//}
);

            var handler = new GetHediyelikQueryHandler(_hediyelikRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.HediyelikId.Should().Be(1);

        }

        [Test]
        public async Task Hediyelik_GetQueries_Success()
        {
            //Arrange
            var query = new GetHediyeliksQuery();

            _hediyelikRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Hediyelik, bool>>>()))
                        .ReturnsAsync(new List<Hediyelik> { new Hediyelik() { /*TODO:propertyler buraya yazılacak HediyelikId = 1, HediyelikName = "test"*/ } });

            var handler = new GetHediyeliksQueryHandler(_hediyelikRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Hediyelik>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Hediyelik_CreateCommand_Success()
        {
            Hediyelik rt = null;
            //Arrange
            var command = new CreateHediyelikCommand();
            //propertyler buraya yazılacak
            //command.HediyelikName = "deneme";

            _hediyelikRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Hediyelik, bool>>>()))
                        .ReturnsAsync(rt);

            _hediyelikRepository.Setup(x => x.Add(It.IsAny<Hediyelik>())).Returns(new Hediyelik());

            var handler = new CreateHediyelikCommandHandler(_hediyelikRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _hediyelikRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Hediyelik_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateHediyelikCommand();
            //propertyler buraya yazılacak 
            //command.HediyelikName = "test";

            _hediyelikRepository.Setup(x => x.Query())
                                           .Returns(new List<Hediyelik> { new Hediyelik() { /*TODO:propertyler buraya yazılacak HediyelikId = 1, HediyelikName = "test"*/ } }.AsQueryable());

            _hediyelikRepository.Setup(x => x.Add(It.IsAny<Hediyelik>())).Returns(new Hediyelik());

            var handler = new CreateHediyelikCommandHandler(_hediyelikRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Hediyelik_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateHediyelikCommand();
            //command.HediyelikName = "test";

            _hediyelikRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Hediyelik, bool>>>()))
                        .ReturnsAsync(new Hediyelik() { /*TODO:propertyler buraya yazılacak HediyelikId = 1, HediyelikName = "deneme"*/ });

            _hediyelikRepository.Setup(x => x.Update(It.IsAny<Hediyelik>())).Returns(new Hediyelik());

            var handler = new UpdateHediyelikCommandHandler(_hediyelikRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _hediyelikRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Hediyelik_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteHediyelikCommand();

            _hediyelikRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Hediyelik, bool>>>()))
                        .ReturnsAsync(new Hediyelik() { /*TODO:propertyler buraya yazılacak HediyelikId = 1, HediyelikName = "deneme"*/});

            _hediyelikRepository.Setup(x => x.Delete(It.IsAny<Hediyelik>()));

            var handler = new DeleteHediyelikCommandHandler(_hediyelikRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _hediyelikRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

