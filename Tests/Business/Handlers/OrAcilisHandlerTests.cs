
using Business.Handlers.OrAcilises.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OrAcilises.Queries.GetOrAcilisQuery;
using Entities.Concrete;
using static Business.Handlers.OrAcilises.Queries.GetOrAcilisesQuery;
using static Business.Handlers.OrAcilises.Commands.CreateOrAcilisCommand;
using Business.Handlers.OrAcilises.Commands;
using Business.Constants;
using static Business.Handlers.OrAcilises.Commands.UpdateOrAcilisCommand;
using static Business.Handlers.OrAcilises.Commands.DeleteOrAcilisCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrAcilisHandlerTests
    {
        Mock<IOrAcilisRepository> _orAcilisRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _orAcilisRepository = new Mock<IOrAcilisRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OrAcilis_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrAcilisQuery();

            _orAcilisRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrAcilis, bool>>>())).ReturnsAsync(new OrAcilis()
//propertyler buraya yazılacak
//{																		
//OrAcilisId = 1,
//OrAcilisName = "Test"
//}
);

            var handler = new GetOrAcilisQueryHandler(_orAcilisRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrAcilisId.Should().Be(1);

        }

        [Test]
        public async Task OrAcilis_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrAcilisesQuery();

            _orAcilisRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OrAcilis, bool>>>()))
                        .ReturnsAsync(new List<OrAcilis> { new OrAcilis() { /*TODO:propertyler buraya yazılacak OrAcilisId = 1, OrAcilisName = "test"*/ } });

            var handler = new GetOrAcilisesQueryHandler(_orAcilisRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OrAcilis>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OrAcilis_CreateCommand_Success()
        {
            OrAcilis rt = null;
            //Arrange
            var command = new CreateOrAcilisCommand();
            //propertyler buraya yazılacak
            //command.OrAcilisName = "deneme";

            _orAcilisRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrAcilis, bool>>>()))
                        .ReturnsAsync(rt);

            _orAcilisRepository.Setup(x => x.Add(It.IsAny<OrAcilis>())).Returns(new OrAcilis());

            var handler = new CreateOrAcilisCommandHandler(_orAcilisRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orAcilisRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OrAcilis_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrAcilisCommand();
            //propertyler buraya yazılacak 
            //command.OrAcilisName = "test";

            _orAcilisRepository.Setup(x => x.Query())
                                           .Returns(new List<OrAcilis> { new OrAcilis() { /*TODO:propertyler buraya yazılacak OrAcilisId = 1, OrAcilisName = "test"*/ } }.AsQueryable());

            _orAcilisRepository.Setup(x => x.Add(It.IsAny<OrAcilis>())).Returns(new OrAcilis());

            var handler = new CreateOrAcilisCommandHandler(_orAcilisRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OrAcilis_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrAcilisCommand();
            //command.OrAcilisName = "test";

            _orAcilisRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrAcilis, bool>>>()))
                        .ReturnsAsync(new OrAcilis() { /*TODO:propertyler buraya yazılacak OrAcilisId = 1, OrAcilisName = "deneme"*/ });

            _orAcilisRepository.Setup(x => x.Update(It.IsAny<OrAcilis>())).Returns(new OrAcilis());

            var handler = new UpdateOrAcilisCommandHandler(_orAcilisRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orAcilisRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OrAcilis_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrAcilisCommand();

            _orAcilisRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrAcilis, bool>>>()))
                        .ReturnsAsync(new OrAcilis() { /*TODO:propertyler buraya yazılacak OrAcilisId = 1, OrAcilisName = "deneme"*/});

            _orAcilisRepository.Setup(x => x.Delete(It.IsAny<OrAcilis>()));

            var handler = new DeleteOrAcilisCommandHandler(_orAcilisRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orAcilisRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

