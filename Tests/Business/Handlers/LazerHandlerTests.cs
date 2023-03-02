
using Business.Handlers.Lazers.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Lazers.Queries.GetLazerQuery;
using Entities.Concrete;
using static Business.Handlers.Lazers.Queries.GetLazersQuery;
using static Business.Handlers.Lazers.Commands.CreateLazerCommand;
using Business.Handlers.Lazers.Commands;
using Business.Constants;
using static Business.Handlers.Lazers.Commands.UpdateLazerCommand;
using static Business.Handlers.Lazers.Commands.DeleteLazerCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class LazerHandlerTests
    {
        Mock<ILazerRepository> _lazerRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _lazerRepository = new Mock<ILazerRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Lazer_GetQuery_Success()
        {
            //Arrange
            var query = new GetLazerQuery();

            _lazerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Lazer, bool>>>())).ReturnsAsync(new Lazer()
//propertyler buraya yazılacak
//{																		
//LazerId = 1,
//LazerName = "Test"
//}
);

            var handler = new GetLazerQueryHandler(_lazerRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.LazerId.Should().Be(1);

        }

        [Test]
        public async Task Lazer_GetQueries_Success()
        {
            //Arrange
            var query = new GetLazersQuery();

            _lazerRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Lazer, bool>>>()))
                        .ReturnsAsync(new List<Lazer> { new Lazer() { /*TODO:propertyler buraya yazılacak LazerId = 1, LazerName = "test"*/ } });

            var handler = new GetLazersQueryHandler(_lazerRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Lazer>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Lazer_CreateCommand_Success()
        {
            Lazer rt = null;
            //Arrange
            var command = new CreateLazerCommand();
            //propertyler buraya yazılacak
            //command.LazerName = "deneme";

            _lazerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Lazer, bool>>>()))
                        .ReturnsAsync(rt);

            _lazerRepository.Setup(x => x.Add(It.IsAny<Lazer>())).Returns(new Lazer());

            var handler = new CreateLazerCommandHandler(_lazerRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _lazerRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Lazer_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateLazerCommand();
            //propertyler buraya yazılacak 
            //command.LazerName = "test";

            _lazerRepository.Setup(x => x.Query())
                                           .Returns(new List<Lazer> { new Lazer() { /*TODO:propertyler buraya yazılacak LazerId = 1, LazerName = "test"*/ } }.AsQueryable());

            _lazerRepository.Setup(x => x.Add(It.IsAny<Lazer>())).Returns(new Lazer());

            var handler = new CreateLazerCommandHandler(_lazerRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Lazer_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateLazerCommand();
            //command.LazerName = "test";

            _lazerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Lazer, bool>>>()))
                        .ReturnsAsync(new Lazer() { /*TODO:propertyler buraya yazılacak LazerId = 1, LazerName = "deneme"*/ });

            _lazerRepository.Setup(x => x.Update(It.IsAny<Lazer>())).Returns(new Lazer());

            var handler = new UpdateLazerCommandHandler(_lazerRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _lazerRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Lazer_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteLazerCommand();

            _lazerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Lazer, bool>>>()))
                        .ReturnsAsync(new Lazer() { /*TODO:propertyler buraya yazılacak LazerId = 1, LazerName = "deneme"*/});

            _lazerRepository.Setup(x => x.Delete(It.IsAny<Lazer>()));

            var handler = new DeleteLazerCommandHandler(_lazerRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _lazerRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

