
using Business.Handlers.Yiyeceks.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Yiyeceks.Queries.GetYiyecekQuery;
using Entities.Concrete;
using static Business.Handlers.Yiyeceks.Queries.GetYiyeceksQuery;
using static Business.Handlers.Yiyeceks.Commands.CreateYiyecekCommand;
using Business.Handlers.Yiyeceks.Commands;
using Business.Constants;
using static Business.Handlers.Yiyeceks.Commands.UpdateYiyecekCommand;
using static Business.Handlers.Yiyeceks.Commands.DeleteYiyecekCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class YiyecekHandlerTests
    {
        Mock<IYiyecekRepository> _yiyecekRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _yiyecekRepository = new Mock<IYiyecekRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Yiyecek_GetQuery_Success()
        {
            //Arrange
            var query = new GetYiyecekQuery();

            _yiyecekRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Yiyecek, bool>>>())).ReturnsAsync(new Yiyecek()
//propertyler buraya yazılacak
//{																		
//YiyecekId = 1,
//YiyecekName = "Test"
//}
);

            var handler = new GetYiyecekQueryHandler(_yiyecekRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.YiyecekId.Should().Be(1);

        }

        [Test]
        public async Task Yiyecek_GetQueries_Success()
        {
            //Arrange
            var query = new GetYiyeceksQuery();

            _yiyecekRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Yiyecek, bool>>>()))
                        .ReturnsAsync(new List<Yiyecek> { new Yiyecek() { /*TODO:propertyler buraya yazılacak YiyecekId = 1, YiyecekName = "test"*/ } });

            var handler = new GetYiyeceksQueryHandler(_yiyecekRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Yiyecek>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Yiyecek_CreateCommand_Success()
        {
            Yiyecek rt = null;
            //Arrange
            var command = new CreateYiyecekCommand();
            //propertyler buraya yazılacak
            //command.YiyecekName = "deneme";

            _yiyecekRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Yiyecek, bool>>>()))
                        .ReturnsAsync(rt);

            _yiyecekRepository.Setup(x => x.Add(It.IsAny<Yiyecek>())).Returns(new Yiyecek());

            var handler = new CreateYiyecekCommandHandler(_yiyecekRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _yiyecekRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Yiyecek_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateYiyecekCommand();
            //propertyler buraya yazılacak 
            //command.YiyecekName = "test";

            _yiyecekRepository.Setup(x => x.Query())
                                           .Returns(new List<Yiyecek> { new Yiyecek() { /*TODO:propertyler buraya yazılacak YiyecekId = 1, YiyecekName = "test"*/ } }.AsQueryable());

            _yiyecekRepository.Setup(x => x.Add(It.IsAny<Yiyecek>())).Returns(new Yiyecek());

            var handler = new CreateYiyecekCommandHandler(_yiyecekRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Yiyecek_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateYiyecekCommand();
            //command.YiyecekName = "test";

            _yiyecekRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Yiyecek, bool>>>()))
                        .ReturnsAsync(new Yiyecek() { /*TODO:propertyler buraya yazılacak YiyecekId = 1, YiyecekName = "deneme"*/ });

            _yiyecekRepository.Setup(x => x.Update(It.IsAny<Yiyecek>())).Returns(new Yiyecek());

            var handler = new UpdateYiyecekCommandHandler(_yiyecekRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _yiyecekRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Yiyecek_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteYiyecekCommand();

            _yiyecekRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Yiyecek, bool>>>()))
                        .ReturnsAsync(new Yiyecek() { /*TODO:propertyler buraya yazılacak YiyecekId = 1, YiyecekName = "deneme"*/});

            _yiyecekRepository.Setup(x => x.Delete(It.IsAny<Yiyecek>()));

            var handler = new DeleteYiyecekCommandHandler(_yiyecekRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _yiyecekRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

