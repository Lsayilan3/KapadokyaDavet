
using Business.Handlers.OrSirketEglences.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OrSirketEglences.Queries.GetOrSirketEglenceQuery;
using Entities.Concrete;
using static Business.Handlers.OrSirketEglences.Queries.GetOrSirketEglencesQuery;
using static Business.Handlers.OrSirketEglences.Commands.CreateOrSirketEglenceCommand;
using Business.Handlers.OrSirketEglences.Commands;
using Business.Constants;
using static Business.Handlers.OrSirketEglences.Commands.UpdateOrSirketEglenceCommand;
using static Business.Handlers.OrSirketEglences.Commands.DeleteOrSirketEglenceCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrSirketEglenceHandlerTests
    {
        Mock<IOrSirketEglenceRepository> _orSirketEglenceRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _orSirketEglenceRepository = new Mock<IOrSirketEglenceRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OrSirketEglence_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrSirketEglenceQuery();

            _orSirketEglenceRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrSirketEglence, bool>>>())).ReturnsAsync(new OrSirketEglence()
//propertyler buraya yazılacak
//{																		
//OrSirketEglenceId = 1,
//OrSirketEglenceName = "Test"
//}
);

            var handler = new GetOrSirketEglenceQueryHandler(_orSirketEglenceRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrSirketEglenceId.Should().Be(1);

        }

        [Test]
        public async Task OrSirketEglence_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrSirketEglencesQuery();

            _orSirketEglenceRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OrSirketEglence, bool>>>()))
                        .ReturnsAsync(new List<OrSirketEglence> { new OrSirketEglence() { /*TODO:propertyler buraya yazılacak OrSirketEglenceId = 1, OrSirketEglenceName = "test"*/ } });

            var handler = new GetOrSirketEglencesQueryHandler(_orSirketEglenceRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OrSirketEglence>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OrSirketEglence_CreateCommand_Success()
        {
            OrSirketEglence rt = null;
            //Arrange
            var command = new CreateOrSirketEglenceCommand();
            //propertyler buraya yazılacak
            //command.OrSirketEglenceName = "deneme";

            _orSirketEglenceRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrSirketEglence, bool>>>()))
                        .ReturnsAsync(rt);

            _orSirketEglenceRepository.Setup(x => x.Add(It.IsAny<OrSirketEglence>())).Returns(new OrSirketEglence());

            var handler = new CreateOrSirketEglenceCommandHandler(_orSirketEglenceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orSirketEglenceRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OrSirketEglence_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrSirketEglenceCommand();
            //propertyler buraya yazılacak 
            //command.OrSirketEglenceName = "test";

            _orSirketEglenceRepository.Setup(x => x.Query())
                                           .Returns(new List<OrSirketEglence> { new OrSirketEglence() { /*TODO:propertyler buraya yazılacak OrSirketEglenceId = 1, OrSirketEglenceName = "test"*/ } }.AsQueryable());

            _orSirketEglenceRepository.Setup(x => x.Add(It.IsAny<OrSirketEglence>())).Returns(new OrSirketEglence());

            var handler = new CreateOrSirketEglenceCommandHandler(_orSirketEglenceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OrSirketEglence_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrSirketEglenceCommand();
            //command.OrSirketEglenceName = "test";

            _orSirketEglenceRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrSirketEglence, bool>>>()))
                        .ReturnsAsync(new OrSirketEglence() { /*TODO:propertyler buraya yazılacak OrSirketEglenceId = 1, OrSirketEglenceName = "deneme"*/ });

            _orSirketEglenceRepository.Setup(x => x.Update(It.IsAny<OrSirketEglence>())).Returns(new OrSirketEglence());

            var handler = new UpdateOrSirketEglenceCommandHandler(_orSirketEglenceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orSirketEglenceRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OrSirketEglence_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrSirketEglenceCommand();

            _orSirketEglenceRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrSirketEglence, bool>>>()))
                        .ReturnsAsync(new OrSirketEglence() { /*TODO:propertyler buraya yazılacak OrSirketEglenceId = 1, OrSirketEglenceName = "deneme"*/});

            _orSirketEglenceRepository.Setup(x => x.Delete(It.IsAny<OrSirketEglence>()));

            var handler = new DeleteOrSirketEglenceCommandHandler(_orSirketEglenceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orSirketEglenceRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

