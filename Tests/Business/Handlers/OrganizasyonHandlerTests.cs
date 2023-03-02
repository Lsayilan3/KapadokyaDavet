
using Business.Handlers.Organizasyons.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Organizasyons.Queries.GetOrganizasyonQuery;
using Entities.Concrete;
using static Business.Handlers.Organizasyons.Queries.GetOrganizasyonsQuery;
using static Business.Handlers.Organizasyons.Commands.CreateOrganizasyonCommand;
using Business.Handlers.Organizasyons.Commands;
using Business.Constants;
using static Business.Handlers.Organizasyons.Commands.UpdateOrganizasyonCommand;
using static Business.Handlers.Organizasyons.Commands.DeleteOrganizasyonCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrganizasyonHandlerTests
    {
        Mock<IOrganizasyonRepository> _organizasyonRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _organizasyonRepository = new Mock<IOrganizasyonRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Organizasyon_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrganizasyonQuery();

            _organizasyonRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Organizasyon, bool>>>())).ReturnsAsync(new Organizasyon()
//propertyler buraya yazılacak
//{																		
//OrganizasyonId = 1,
//OrganizasyonName = "Test"
//}
);

            var handler = new GetOrganizasyonQueryHandler(_organizasyonRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrganizasyonId.Should().Be(1);

        }

        [Test]
        public async Task Organizasyon_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrganizasyonsQuery();

            _organizasyonRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Organizasyon, bool>>>()))
                        .ReturnsAsync(new List<Organizasyon> { new Organizasyon() { /*TODO:propertyler buraya yazılacak OrganizasyonId = 1, OrganizasyonName = "test"*/ } });

            var handler = new GetOrganizasyonsQueryHandler(_organizasyonRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Organizasyon>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Organizasyon_CreateCommand_Success()
        {
            Organizasyon rt = null;
            //Arrange
            var command = new CreateOrganizasyonCommand();
            //propertyler buraya yazılacak
            //command.OrganizasyonName = "deneme";

            _organizasyonRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Organizasyon, bool>>>()))
                        .ReturnsAsync(rt);

            _organizasyonRepository.Setup(x => x.Add(It.IsAny<Organizasyon>())).Returns(new Organizasyon());

            var handler = new CreateOrganizasyonCommandHandler(_organizasyonRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _organizasyonRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Organizasyon_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrganizasyonCommand();
            //propertyler buraya yazılacak 
            //command.OrganizasyonName = "test";

            _organizasyonRepository.Setup(x => x.Query())
                                           .Returns(new List<Organizasyon> { new Organizasyon() { /*TODO:propertyler buraya yazılacak OrganizasyonId = 1, OrganizasyonName = "test"*/ } }.AsQueryable());

            _organizasyonRepository.Setup(x => x.Add(It.IsAny<Organizasyon>())).Returns(new Organizasyon());

            var handler = new CreateOrganizasyonCommandHandler(_organizasyonRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Organizasyon_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrganizasyonCommand();
            //command.OrganizasyonName = "test";

            _organizasyonRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Organizasyon, bool>>>()))
                        .ReturnsAsync(new Organizasyon() { /*TODO:propertyler buraya yazılacak OrganizasyonId = 1, OrganizasyonName = "deneme"*/ });

            _organizasyonRepository.Setup(x => x.Update(It.IsAny<Organizasyon>())).Returns(new Organizasyon());

            var handler = new UpdateOrganizasyonCommandHandler(_organizasyonRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _organizasyonRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Organizasyon_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrganizasyonCommand();

            _organizasyonRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Organizasyon, bool>>>()))
                        .ReturnsAsync(new Organizasyon() { /*TODO:propertyler buraya yazılacak OrganizasyonId = 1, OrganizasyonName = "deneme"*/});

            _organizasyonRepository.Setup(x => x.Delete(It.IsAny<Organizasyon>()));

            var handler = new DeleteOrganizasyonCommandHandler(_organizasyonRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _organizasyonRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

