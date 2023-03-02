
using Business.Handlers.Ensonuruns.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Ensonuruns.Queries.GetEnsonurunQuery;
using Entities.Concrete;
using static Business.Handlers.Ensonuruns.Queries.GetEnsonurunsQuery;
using static Business.Handlers.Ensonuruns.Commands.CreateEnsonurunCommand;
using Business.Handlers.Ensonuruns.Commands;
using Business.Constants;
using static Business.Handlers.Ensonuruns.Commands.UpdateEnsonurunCommand;
using static Business.Handlers.Ensonuruns.Commands.DeleteEnsonurunCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class EnsonurunHandlerTests
    {
        Mock<IEnsonurunRepository> _ensonurunRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _ensonurunRepository = new Mock<IEnsonurunRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Ensonurun_GetQuery_Success()
        {
            //Arrange
            var query = new GetEnsonurunQuery();

            _ensonurunRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Ensonurun, bool>>>())).ReturnsAsync(new Ensonurun()
//propertyler buraya yazılacak
//{																		
//EnsonurunId = 1,
//EnsonurunName = "Test"
//}
);

            var handler = new GetEnsonurunQueryHandler(_ensonurunRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.EnsonurunId.Should().Be(1);

        }

        [Test]
        public async Task Ensonurun_GetQueries_Success()
        {
            //Arrange
            var query = new GetEnsonurunsQuery();

            _ensonurunRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Ensonurun, bool>>>()))
                        .ReturnsAsync(new List<Ensonurun> { new Ensonurun() { /*TODO:propertyler buraya yazılacak EnsonurunId = 1, EnsonurunName = "test"*/ } });

            var handler = new GetEnsonurunsQueryHandler(_ensonurunRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Ensonurun>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Ensonurun_CreateCommand_Success()
        {
            Ensonurun rt = null;
            //Arrange
            var command = new CreateEnsonurunCommand();
            //propertyler buraya yazılacak
            //command.EnsonurunName = "deneme";

            _ensonurunRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Ensonurun, bool>>>()))
                        .ReturnsAsync(rt);

            _ensonurunRepository.Setup(x => x.Add(It.IsAny<Ensonurun>())).Returns(new Ensonurun());

            var handler = new CreateEnsonurunCommandHandler(_ensonurunRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _ensonurunRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Ensonurun_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateEnsonurunCommand();
            //propertyler buraya yazılacak 
            //command.EnsonurunName = "test";

            _ensonurunRepository.Setup(x => x.Query())
                                           .Returns(new List<Ensonurun> { new Ensonurun() { /*TODO:propertyler buraya yazılacak EnsonurunId = 1, EnsonurunName = "test"*/ } }.AsQueryable());

            _ensonurunRepository.Setup(x => x.Add(It.IsAny<Ensonurun>())).Returns(new Ensonurun());

            var handler = new CreateEnsonurunCommandHandler(_ensonurunRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Ensonurun_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateEnsonurunCommand();
            //command.EnsonurunName = "test";

            _ensonurunRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Ensonurun, bool>>>()))
                        .ReturnsAsync(new Ensonurun() { /*TODO:propertyler buraya yazılacak EnsonurunId = 1, EnsonurunName = "deneme"*/ });

            _ensonurunRepository.Setup(x => x.Update(It.IsAny<Ensonurun>())).Returns(new Ensonurun());

            var handler = new UpdateEnsonurunCommandHandler(_ensonurunRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _ensonurunRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Ensonurun_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteEnsonurunCommand();

            _ensonurunRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Ensonurun, bool>>>()))
                        .ReturnsAsync(new Ensonurun() { /*TODO:propertyler buraya yazılacak EnsonurunId = 1, EnsonurunName = "deneme"*/});

            _ensonurunRepository.Setup(x => x.Delete(It.IsAny<Ensonurun>()));

            var handler = new DeleteEnsonurunCommandHandler(_ensonurunRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _ensonurunRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

