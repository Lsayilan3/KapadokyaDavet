
using Business.Handlers.Sliders.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Sliders.Queries.GetSliderQuery;
using Entities.Concrete;
using static Business.Handlers.Sliders.Queries.GetSlidersQuery;
using static Business.Handlers.Sliders.Commands.CreateSliderCommand;
using Business.Handlers.Sliders.Commands;
using Business.Constants;
using static Business.Handlers.Sliders.Commands.UpdateSliderCommand;
using static Business.Handlers.Sliders.Commands.DeleteSliderCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class SliderHandlerTests
    {
        Mock<ISliderRepository> _sliderRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _sliderRepository = new Mock<ISliderRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Slider_GetQuery_Success()
        {
            //Arrange
            var query = new GetSliderQuery();

            _sliderRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Slider, bool>>>())).ReturnsAsync(new Slider()
//propertyler buraya yazılacak
//{																		
//SliderId = 1,
//SliderName = "Test"
//}
);

            var handler = new GetSliderQueryHandler(_sliderRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.SliderId.Should().Be(1);

        }

        [Test]
        public async Task Slider_GetQueries_Success()
        {
            //Arrange
            var query = new GetSlidersQuery();

            _sliderRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Slider, bool>>>()))
                        .ReturnsAsync(new List<Slider> { new Slider() { /*TODO:propertyler buraya yazılacak SliderId = 1, SliderName = "test"*/ } });

            var handler = new GetSlidersQueryHandler(_sliderRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Slider>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Slider_CreateCommand_Success()
        {
            Slider rt = null;
            //Arrange
            var command = new CreateSliderCommand();
            //propertyler buraya yazılacak
            //command.SliderName = "deneme";

            _sliderRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Slider, bool>>>()))
                        .ReturnsAsync(rt);

            _sliderRepository.Setup(x => x.Add(It.IsAny<Slider>())).Returns(new Slider());

            var handler = new CreateSliderCommandHandler(_sliderRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sliderRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Slider_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateSliderCommand();
            //propertyler buraya yazılacak 
            //command.SliderName = "test";

            _sliderRepository.Setup(x => x.Query())
                                           .Returns(new List<Slider> { new Slider() { /*TODO:propertyler buraya yazılacak SliderId = 1, SliderName = "test"*/ } }.AsQueryable());

            _sliderRepository.Setup(x => x.Add(It.IsAny<Slider>())).Returns(new Slider());

            var handler = new CreateSliderCommandHandler(_sliderRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Slider_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateSliderCommand();
            //command.SliderName = "test";

            _sliderRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Slider, bool>>>()))
                        .ReturnsAsync(new Slider() { /*TODO:propertyler buraya yazılacak SliderId = 1, SliderName = "deneme"*/ });

            _sliderRepository.Setup(x => x.Update(It.IsAny<Slider>())).Returns(new Slider());

            var handler = new UpdateSliderCommandHandler(_sliderRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sliderRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Slider_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteSliderCommand();

            _sliderRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Slider, bool>>>()))
                        .ReturnsAsync(new Slider() { /*TODO:propertyler buraya yazılacak SliderId = 1, SliderName = "deneme"*/});

            _sliderRepository.Setup(x => x.Delete(It.IsAny<Slider>()));

            var handler = new DeleteSliderCommandHandler(_sliderRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sliderRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

