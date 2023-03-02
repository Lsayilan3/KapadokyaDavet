
using Business.Handlers.OrBabies.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OrBabies.Queries.GetOrBabyQuery;
using Entities.Concrete;
using static Business.Handlers.OrBabies.Queries.GetOrBabiesQuery;
using static Business.Handlers.OrBabies.Commands.CreateOrBabyCommand;
using Business.Handlers.OrBabies.Commands;
using Business.Constants;
using static Business.Handlers.OrBabies.Commands.UpdateOrBabyCommand;
using static Business.Handlers.OrBabies.Commands.DeleteOrBabyCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrBabyHandlerTests
    {
        Mock<IOrBabyRepository> _orBabyRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _orBabyRepository = new Mock<IOrBabyRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OrBaby_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrBabyQuery();

            _orBabyRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrBaby, bool>>>())).ReturnsAsync(new OrBaby()
//propertyler buraya yazılacak
//{																		
//OrBabyId = 1,
//OrBabyName = "Test"
//}
);

            var handler = new GetOrBabyQueryHandler(_orBabyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrBabyId.Should().Be(1);

        }

        [Test]
        public async Task OrBaby_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrBabiesQuery();

            _orBabyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OrBaby, bool>>>()))
                        .ReturnsAsync(new List<OrBaby> { new OrBaby() { /*TODO:propertyler buraya yazılacak OrBabyId = 1, OrBabyName = "test"*/ } });

            var handler = new GetOrBabiesQueryHandler(_orBabyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OrBaby>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OrBaby_CreateCommand_Success()
        {
            OrBaby rt = null;
            //Arrange
            var command = new CreateOrBabyCommand();
            //propertyler buraya yazılacak
            //command.OrBabyName = "deneme";

            _orBabyRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrBaby, bool>>>()))
                        .ReturnsAsync(rt);

            _orBabyRepository.Setup(x => x.Add(It.IsAny<OrBaby>())).Returns(new OrBaby());

            var handler = new CreateOrBabyCommandHandler(_orBabyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orBabyRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OrBaby_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrBabyCommand();
            //propertyler buraya yazılacak 
            //command.OrBabyName = "test";

            _orBabyRepository.Setup(x => x.Query())
                                           .Returns(new List<OrBaby> { new OrBaby() { /*TODO:propertyler buraya yazılacak OrBabyId = 1, OrBabyName = "test"*/ } }.AsQueryable());

            _orBabyRepository.Setup(x => x.Add(It.IsAny<OrBaby>())).Returns(new OrBaby());

            var handler = new CreateOrBabyCommandHandler(_orBabyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OrBaby_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrBabyCommand();
            //command.OrBabyName = "test";

            _orBabyRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrBaby, bool>>>()))
                        .ReturnsAsync(new OrBaby() { /*TODO:propertyler buraya yazılacak OrBabyId = 1, OrBabyName = "deneme"*/ });

            _orBabyRepository.Setup(x => x.Update(It.IsAny<OrBaby>())).Returns(new OrBaby());

            var handler = new UpdateOrBabyCommandHandler(_orBabyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orBabyRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OrBaby_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrBabyCommand();

            _orBabyRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrBaby, bool>>>()))
                        .ReturnsAsync(new OrBaby() { /*TODO:propertyler buraya yazılacak OrBabyId = 1, OrBabyName = "deneme"*/});

            _orBabyRepository.Setup(x => x.Delete(It.IsAny<OrBaby>()));

            var handler = new DeleteOrBabyCommandHandler(_orBabyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orBabyRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

