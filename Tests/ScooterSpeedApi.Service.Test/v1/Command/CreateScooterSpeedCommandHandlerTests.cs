using FakeItEasy;
using FluentAssertions;
using ScooterSpeedApi.Data.Repository.v1;
using ScooterSpeedApi.Domain;
using ScooterSpeedApi.Service.v1.Command;
using Xunit;

namespace ScooterSpeedApi.Service.Test.v1.Command
{
    public class CreateScooterSpeedCommandHandlerTests
    {
        private readonly IScooterSpeedRepository _scooterSpeedRepository;
        private readonly CreateScooterSpeedCommandHandler _testee;

        public CreateScooterSpeedCommandHandlerTests()
        {
            _scooterSpeedRepository = A.Fake<IScooterSpeedRepository>();
            _testee = new CreateScooterSpeedCommandHandler(_scooterSpeedRepository);
        }

        [Fact]
        public async void Handle_ShouldReturnCreatedScooterSpeed()
        {
            A.CallTo(() => _scooterSpeedRepository.AddAsync(A<ScooterSpeed>._)).Returns(new ScooterSpeed { Speed = 55});

            var result = await _testee.Handle(new CreateScooterSpeedCommand(), default);

            result.Should().BeOfType<ScooterSpeed>();
            result.Speed.Should().Be(55);
        }

        [Fact]
        public async void Handle_ShouldCallRepositoryAddAsync()
        {
            await _testee.Handle(new CreateScooterSpeedCommand(), default);

            A.CallTo(() => _scooterSpeedRepository.AddAsync(A<ScooterSpeed>._)).MustHaveHappenedOnceExactly();
        }
    }
}