
using Business.Handlers.SpotCategoryies.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.SpotCategoryies.Queries.GetSpotCategoryyQuery;
using Entities.Concrete;
using static Business.Handlers.SpotCategoryies.Queries.GetSpotCategoryiesQuery;
using static Business.Handlers.SpotCategoryies.Commands.CreateSpotCategoryyCommand;
using Business.Handlers.SpotCategoryies.Commands;
using Business.Constants;
using static Business.Handlers.SpotCategoryies.Commands.UpdateSpotCategoryyCommand;
using static Business.Handlers.SpotCategoryies.Commands.DeleteSpotCategoryyCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class SpotCategoryyHandlerTests
    {
        Mock<ISpotCategoryyRepository> _spotCategoryyRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _spotCategoryyRepository = new Mock<ISpotCategoryyRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task SpotCategoryy_GetQuery_Success()
        {
            //Arrange
            var query = new GetSpotCategoryyQuery();

            _spotCategoryyRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<SpotCategoryy, bool>>>())).ReturnsAsync(new SpotCategoryy()
//propertyler buraya yazılacak
//{																		
//SpotCategoryyId = 1,
//SpotCategoryyName = "Test"
//}
);

            var handler = new GetSpotCategoryyQueryHandler(_spotCategoryyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.SpotCategoryyId.Should().Be(1);

        }

        [Test]
        public async Task SpotCategoryy_GetQueries_Success()
        {
            //Arrange
            var query = new GetSpotCategoryiesQuery();

            _spotCategoryyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<SpotCategoryy, bool>>>()))
                        .ReturnsAsync(new List<SpotCategoryy> { new SpotCategoryy() { /*TODO:propertyler buraya yazılacak SpotCategoryyId = 1, SpotCategoryyName = "test"*/ } });

            var handler = new GetSpotCategoryiesQueryHandler(_spotCategoryyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<SpotCategoryy>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task SpotCategoryy_CreateCommand_Success()
        {
            SpotCategoryy rt = null;
            //Arrange
            var command = new CreateSpotCategoryyCommand();
            //propertyler buraya yazılacak
            //command.SpotCategoryyName = "deneme";

            _spotCategoryyRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<SpotCategoryy, bool>>>()))
                        .ReturnsAsync(rt);

            _spotCategoryyRepository.Setup(x => x.Add(It.IsAny<SpotCategoryy>())).Returns(new SpotCategoryy());

            var handler = new CreateSpotCategoryyCommandHandler(_spotCategoryyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _spotCategoryyRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task SpotCategoryy_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateSpotCategoryyCommand();
            //propertyler buraya yazılacak 
            //command.SpotCategoryyName = "test";

            _spotCategoryyRepository.Setup(x => x.Query())
                                           .Returns(new List<SpotCategoryy> { new SpotCategoryy() { /*TODO:propertyler buraya yazılacak SpotCategoryyId = 1, SpotCategoryyName = "test"*/ } }.AsQueryable());

            _spotCategoryyRepository.Setup(x => x.Add(It.IsAny<SpotCategoryy>())).Returns(new SpotCategoryy());

            var handler = new CreateSpotCategoryyCommandHandler(_spotCategoryyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task SpotCategoryy_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateSpotCategoryyCommand();
            //command.SpotCategoryyName = "test";

            _spotCategoryyRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<SpotCategoryy, bool>>>()))
                        .ReturnsAsync(new SpotCategoryy() { /*TODO:propertyler buraya yazılacak SpotCategoryyId = 1, SpotCategoryyName = "deneme"*/ });

            _spotCategoryyRepository.Setup(x => x.Update(It.IsAny<SpotCategoryy>())).Returns(new SpotCategoryy());

            var handler = new UpdateSpotCategoryyCommandHandler(_spotCategoryyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _spotCategoryyRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task SpotCategoryy_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteSpotCategoryyCommand();

            _spotCategoryyRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<SpotCategoryy, bool>>>()))
                        .ReturnsAsync(new SpotCategoryy() { /*TODO:propertyler buraya yazılacak SpotCategoryyId = 1, SpotCategoryyName = "deneme"*/});

            _spotCategoryyRepository.Setup(x => x.Delete(It.IsAny<SpotCategoryy>()));

            var handler = new DeleteSpotCategoryyCommandHandler(_spotCategoryyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _spotCategoryyRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

