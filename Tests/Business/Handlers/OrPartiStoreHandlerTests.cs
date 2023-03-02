
using Business.Handlers.OrPartiStores.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OrPartiStores.Queries.GetOrPartiStoreQuery;
using Entities.Concrete;
using static Business.Handlers.OrPartiStores.Queries.GetOrPartiStoresQuery;
using static Business.Handlers.OrPartiStores.Commands.CreateOrPartiStoreCommand;
using Business.Handlers.OrPartiStores.Commands;
using Business.Constants;
using static Business.Handlers.OrPartiStores.Commands.UpdateOrPartiStoreCommand;
using static Business.Handlers.OrPartiStores.Commands.DeleteOrPartiStoreCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrPartiStoreHandlerTests
    {
        Mock<IOrPartiStoreRepository> _orPartiStoreRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _orPartiStoreRepository = new Mock<IOrPartiStoreRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OrPartiStore_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrPartiStoreQuery();

            _orPartiStoreRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrPartiStore, bool>>>())).ReturnsAsync(new OrPartiStore()
//propertyler buraya yazılacak
//{																		
//OrPartiStoreId = 1,
//OrPartiStoreName = "Test"
//}
);

            var handler = new GetOrPartiStoreQueryHandler(_orPartiStoreRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrPartiStoreId.Should().Be(1);

        }

        [Test]
        public async Task OrPartiStore_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrPartiStoresQuery();

            _orPartiStoreRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OrPartiStore, bool>>>()))
                        .ReturnsAsync(new List<OrPartiStore> { new OrPartiStore() { /*TODO:propertyler buraya yazılacak OrPartiStoreId = 1, OrPartiStoreName = "test"*/ } });

            var handler = new GetOrPartiStoresQueryHandler(_orPartiStoreRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OrPartiStore>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OrPartiStore_CreateCommand_Success()
        {
            OrPartiStore rt = null;
            //Arrange
            var command = new CreateOrPartiStoreCommand();
            //propertyler buraya yazılacak
            //command.OrPartiStoreName = "deneme";

            _orPartiStoreRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrPartiStore, bool>>>()))
                        .ReturnsAsync(rt);

            _orPartiStoreRepository.Setup(x => x.Add(It.IsAny<OrPartiStore>())).Returns(new OrPartiStore());

            var handler = new CreateOrPartiStoreCommandHandler(_orPartiStoreRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orPartiStoreRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OrPartiStore_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrPartiStoreCommand();
            //propertyler buraya yazılacak 
            //command.OrPartiStoreName = "test";

            _orPartiStoreRepository.Setup(x => x.Query())
                                           .Returns(new List<OrPartiStore> { new OrPartiStore() { /*TODO:propertyler buraya yazılacak OrPartiStoreId = 1, OrPartiStoreName = "test"*/ } }.AsQueryable());

            _orPartiStoreRepository.Setup(x => x.Add(It.IsAny<OrPartiStore>())).Returns(new OrPartiStore());

            var handler = new CreateOrPartiStoreCommandHandler(_orPartiStoreRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OrPartiStore_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrPartiStoreCommand();
            //command.OrPartiStoreName = "test";

            _orPartiStoreRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrPartiStore, bool>>>()))
                        .ReturnsAsync(new OrPartiStore() { /*TODO:propertyler buraya yazılacak OrPartiStoreId = 1, OrPartiStoreName = "deneme"*/ });

            _orPartiStoreRepository.Setup(x => x.Update(It.IsAny<OrPartiStore>())).Returns(new OrPartiStore());

            var handler = new UpdateOrPartiStoreCommandHandler(_orPartiStoreRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orPartiStoreRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OrPartiStore_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrPartiStoreCommand();

            _orPartiStoreRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrPartiStore, bool>>>()))
                        .ReturnsAsync(new OrPartiStore() { /*TODO:propertyler buraya yazılacak OrPartiStoreId = 1, OrPartiStoreName = "deneme"*/});

            _orPartiStoreRepository.Setup(x => x.Delete(It.IsAny<OrPartiStore>()));

            var handler = new DeleteOrPartiStoreCommandHandler(_orPartiStoreRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orPartiStoreRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

