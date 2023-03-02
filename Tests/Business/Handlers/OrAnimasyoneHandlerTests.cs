
using Business.Handlers.OrAnimasyones.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OrAnimasyones.Queries.GetOrAnimasyoneQuery;
using Entities.Concrete;
using static Business.Handlers.OrAnimasyones.Queries.GetOrAnimasyonesQuery;
using static Business.Handlers.OrAnimasyones.Commands.CreateOrAnimasyoneCommand;
using Business.Handlers.OrAnimasyones.Commands;
using Business.Constants;
using static Business.Handlers.OrAnimasyones.Commands.UpdateOrAnimasyoneCommand;
using static Business.Handlers.OrAnimasyones.Commands.DeleteOrAnimasyoneCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrAnimasyoneHandlerTests
    {
        Mock<IOrAnimasyoneRepository> _orAnimasyoneRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _orAnimasyoneRepository = new Mock<IOrAnimasyoneRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OrAnimasyone_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrAnimasyoneQuery();

            _orAnimasyoneRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrAnimasyone, bool>>>())).ReturnsAsync(new OrAnimasyone()
//propertyler buraya yazılacak
//{																		
//OrAnimasyoneId = 1,
//OrAnimasyoneName = "Test"
//}
);

            var handler = new GetOrAnimasyoneQueryHandler(_orAnimasyoneRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrAnimasyoneId.Should().Be(1);

        }

        [Test]
        public async Task OrAnimasyone_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrAnimasyonesQuery();

            _orAnimasyoneRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OrAnimasyone, bool>>>()))
                        .ReturnsAsync(new List<OrAnimasyone> { new OrAnimasyone() { /*TODO:propertyler buraya yazılacak OrAnimasyoneId = 1, OrAnimasyoneName = "test"*/ } });

            var handler = new GetOrAnimasyonesQueryHandler(_orAnimasyoneRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OrAnimasyone>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OrAnimasyone_CreateCommand_Success()
        {
            OrAnimasyone rt = null;
            //Arrange
            var command = new CreateOrAnimasyoneCommand();
            //propertyler buraya yazılacak
            //command.OrAnimasyoneName = "deneme";

            _orAnimasyoneRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrAnimasyone, bool>>>()))
                        .ReturnsAsync(rt);

            _orAnimasyoneRepository.Setup(x => x.Add(It.IsAny<OrAnimasyone>())).Returns(new OrAnimasyone());

            var handler = new CreateOrAnimasyoneCommandHandler(_orAnimasyoneRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orAnimasyoneRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OrAnimasyone_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrAnimasyoneCommand();
            //propertyler buraya yazılacak 
            //command.OrAnimasyoneName = "test";

            _orAnimasyoneRepository.Setup(x => x.Query())
                                           .Returns(new List<OrAnimasyone> { new OrAnimasyone() { /*TODO:propertyler buraya yazılacak OrAnimasyoneId = 1, OrAnimasyoneName = "test"*/ } }.AsQueryable());

            _orAnimasyoneRepository.Setup(x => x.Add(It.IsAny<OrAnimasyone>())).Returns(new OrAnimasyone());

            var handler = new CreateOrAnimasyoneCommandHandler(_orAnimasyoneRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OrAnimasyone_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrAnimasyoneCommand();
            //command.OrAnimasyoneName = "test";

            _orAnimasyoneRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrAnimasyone, bool>>>()))
                        .ReturnsAsync(new OrAnimasyone() { /*TODO:propertyler buraya yazılacak OrAnimasyoneId = 1, OrAnimasyoneName = "deneme"*/ });

            _orAnimasyoneRepository.Setup(x => x.Update(It.IsAny<OrAnimasyone>())).Returns(new OrAnimasyone());

            var handler = new UpdateOrAnimasyoneCommandHandler(_orAnimasyoneRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orAnimasyoneRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OrAnimasyone_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrAnimasyoneCommand();

            _orAnimasyoneRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrAnimasyone, bool>>>()))
                        .ReturnsAsync(new OrAnimasyone() { /*TODO:propertyler buraya yazılacak OrAnimasyoneId = 1, OrAnimasyoneName = "deneme"*/});

            _orAnimasyoneRepository.Setup(x => x.Delete(It.IsAny<OrAnimasyone>()));

            var handler = new DeleteOrAnimasyoneCommandHandler(_orAnimasyoneRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orAnimasyoneRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

