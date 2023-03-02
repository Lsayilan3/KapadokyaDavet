
using Business.Handlers.OrCaterings.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OrCaterings.Queries.GetOrCateringQuery;
using Entities.Concrete;
using static Business.Handlers.OrCaterings.Queries.GetOrCateringsQuery;
using static Business.Handlers.OrCaterings.Commands.CreateOrCateringCommand;
using Business.Handlers.OrCaterings.Commands;
using Business.Constants;
using static Business.Handlers.OrCaterings.Commands.UpdateOrCateringCommand;
using static Business.Handlers.OrCaterings.Commands.DeleteOrCateringCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrCateringHandlerTests
    {
        Mock<IOrCateringRepository> _orCateringRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _orCateringRepository = new Mock<IOrCateringRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OrCatering_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrCateringQuery();

            _orCateringRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrCatering, bool>>>())).ReturnsAsync(new OrCatering()
//propertyler buraya yazılacak
//{																		
//OrCateringId = 1,
//OrCateringName = "Test"
//}
);

            var handler = new GetOrCateringQueryHandler(_orCateringRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrCateringId.Should().Be(1);

        }

        [Test]
        public async Task OrCatering_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrCateringsQuery();

            _orCateringRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OrCatering, bool>>>()))
                        .ReturnsAsync(new List<OrCatering> { new OrCatering() { /*TODO:propertyler buraya yazılacak OrCateringId = 1, OrCateringName = "test"*/ } });

            var handler = new GetOrCateringsQueryHandler(_orCateringRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OrCatering>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OrCatering_CreateCommand_Success()
        {
            OrCatering rt = null;
            //Arrange
            var command = new CreateOrCateringCommand();
            //propertyler buraya yazılacak
            //command.OrCateringName = "deneme";

            _orCateringRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrCatering, bool>>>()))
                        .ReturnsAsync(rt);

            _orCateringRepository.Setup(x => x.Add(It.IsAny<OrCatering>())).Returns(new OrCatering());

            var handler = new CreateOrCateringCommandHandler(_orCateringRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orCateringRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OrCatering_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrCateringCommand();
            //propertyler buraya yazılacak 
            //command.OrCateringName = "test";

            _orCateringRepository.Setup(x => x.Query())
                                           .Returns(new List<OrCatering> { new OrCatering() { /*TODO:propertyler buraya yazılacak OrCateringId = 1, OrCateringName = "test"*/ } }.AsQueryable());

            _orCateringRepository.Setup(x => x.Add(It.IsAny<OrCatering>())).Returns(new OrCatering());

            var handler = new CreateOrCateringCommandHandler(_orCateringRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OrCatering_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrCateringCommand();
            //command.OrCateringName = "test";

            _orCateringRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrCatering, bool>>>()))
                        .ReturnsAsync(new OrCatering() { /*TODO:propertyler buraya yazılacak OrCateringId = 1, OrCateringName = "deneme"*/ });

            _orCateringRepository.Setup(x => x.Update(It.IsAny<OrCatering>())).Returns(new OrCatering());

            var handler = new UpdateOrCateringCommandHandler(_orCateringRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orCateringRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OrCatering_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrCateringCommand();

            _orCateringRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrCatering, bool>>>()))
                        .ReturnsAsync(new OrCatering() { /*TODO:propertyler buraya yazılacak OrCateringId = 1, OrCateringName = "deneme"*/});

            _orCateringRepository.Setup(x => x.Delete(It.IsAny<OrCatering>()));

            var handler = new DeleteOrCateringCommandHandler(_orCateringRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orCateringRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

