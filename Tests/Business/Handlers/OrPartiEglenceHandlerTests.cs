
using Business.Handlers.OrPartiEglences.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OrPartiEglences.Queries.GetOrPartiEglenceQuery;
using Entities.Concrete;
using static Business.Handlers.OrPartiEglences.Queries.GetOrPartiEglencesQuery;
using static Business.Handlers.OrPartiEglences.Commands.CreateOrPartiEglenceCommand;
using Business.Handlers.OrPartiEglences.Commands;
using Business.Constants;
using static Business.Handlers.OrPartiEglences.Commands.UpdateOrPartiEglenceCommand;
using static Business.Handlers.OrPartiEglences.Commands.DeleteOrPartiEglenceCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrPartiEglenceHandlerTests
    {
        Mock<IOrPartiEglenceRepository> _orPartiEglenceRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _orPartiEglenceRepository = new Mock<IOrPartiEglenceRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OrPartiEglence_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrPartiEglenceQuery();

            _orPartiEglenceRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrPartiEglence, bool>>>())).ReturnsAsync(new OrPartiEglence()
//propertyler buraya yazılacak
//{																		
//OrPartiEglenceId = 1,
//OrPartiEglenceName = "Test"
//}
);

            var handler = new GetOrPartiEglenceQueryHandler(_orPartiEglenceRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrPartiEglenceId.Should().Be(1);

        }

        [Test]
        public async Task OrPartiEglence_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrPartiEglencesQuery();

            _orPartiEglenceRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OrPartiEglence, bool>>>()))
                        .ReturnsAsync(new List<OrPartiEglence> { new OrPartiEglence() { /*TODO:propertyler buraya yazılacak OrPartiEglenceId = 1, OrPartiEglenceName = "test"*/ } });

            var handler = new GetOrPartiEglencesQueryHandler(_orPartiEglenceRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OrPartiEglence>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OrPartiEglence_CreateCommand_Success()
        {
            OrPartiEglence rt = null;
            //Arrange
            var command = new CreateOrPartiEglenceCommand();
            //propertyler buraya yazılacak
            //command.OrPartiEglenceName = "deneme";

            _orPartiEglenceRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrPartiEglence, bool>>>()))
                        .ReturnsAsync(rt);

            _orPartiEglenceRepository.Setup(x => x.Add(It.IsAny<OrPartiEglence>())).Returns(new OrPartiEglence());

            var handler = new CreateOrPartiEglenceCommandHandler(_orPartiEglenceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orPartiEglenceRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OrPartiEglence_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrPartiEglenceCommand();
            //propertyler buraya yazılacak 
            //command.OrPartiEglenceName = "test";

            _orPartiEglenceRepository.Setup(x => x.Query())
                                           .Returns(new List<OrPartiEglence> { new OrPartiEglence() { /*TODO:propertyler buraya yazılacak OrPartiEglenceId = 1, OrPartiEglenceName = "test"*/ } }.AsQueryable());

            _orPartiEglenceRepository.Setup(x => x.Add(It.IsAny<OrPartiEglence>())).Returns(new OrPartiEglence());

            var handler = new CreateOrPartiEglenceCommandHandler(_orPartiEglenceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OrPartiEglence_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrPartiEglenceCommand();
            //command.OrPartiEglenceName = "test";

            _orPartiEglenceRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrPartiEglence, bool>>>()))
                        .ReturnsAsync(new OrPartiEglence() { /*TODO:propertyler buraya yazılacak OrPartiEglenceId = 1, OrPartiEglenceName = "deneme"*/ });

            _orPartiEglenceRepository.Setup(x => x.Update(It.IsAny<OrPartiEglence>())).Returns(new OrPartiEglence());

            var handler = new UpdateOrPartiEglenceCommandHandler(_orPartiEglenceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orPartiEglenceRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OrPartiEglence_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrPartiEglenceCommand();

            _orPartiEglenceRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrPartiEglence, bool>>>()))
                        .ReturnsAsync(new OrPartiEglence() { /*TODO:propertyler buraya yazılacak OrPartiEglenceId = 1, OrPartiEglenceName = "deneme"*/});

            _orPartiEglenceRepository.Setup(x => x.Delete(It.IsAny<OrPartiEglence>()));

            var handler = new DeleteOrPartiEglenceCommandHandler(_orPartiEglenceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orPartiEglenceRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

