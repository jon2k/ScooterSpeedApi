using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using ScooterSpeedApi.Data.Database;
using ScooterSpeedApi.Data.Repository.v1;
using ScooterSpeedApi.Data.Test.Infrastructure;
using ScooterSpeedApi.Domain;
using Xunit;

namespace ScooterSpeedApi.Data.Test.Repository.v1
{
    public class RepositoryTests : DatabaseTestBase
    {
        private readonly ScooterSpeedContext _scooterSpeedContext;
        private readonly Repository<ScooterSpeed> _testee;
        private readonly Repository<ScooterSpeed> _testeeFake;
        private readonly ScooterSpeed _newScooterSpeed;

        public RepositoryTests()
        {
            _scooterSpeedContext = A.Fake<ScooterSpeedContext>();
            _testeeFake = new Repository<ScooterSpeed>(_scooterSpeedContext);
            _testee = new Repository<ScooterSpeed>(Context);
            _newScooterSpeed = new ScooterSpeed
            {
                
                Speed = 40,
                Time = DateTime.Now,
                CoordinateX = 50,
                CoordinateY = 50,
                ScooterId = 9
            };
        }
        

        [Fact]
        public void AddAsync_WhenEntityIsNull_ThrowsException()
        {
            _testee.Invoking(x => x.AddAsync(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AddAsync_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _scooterSpeedContext.SaveChangesAsync(default)).Throws<Exception>();

            _testeeFake.Invoking(x => x.AddAsync(new ScooterSpeed())).Should().Throw<Exception>().WithMessage("entity could not be saved Exception of type 'System.Exception' was thrown.");
        }

        [Fact]
        public async void CreateScooterSpeed_WhenScooterSpeedsNotNull_ShouldReturnScooterSpeed()
        {
            var result = await _testee.AddAsync(_newScooterSpeed);

            result.Should().BeOfType<ScooterSpeed>();
        }

        [Fact]
        public async void reateScooterSpeed_WhenScooterSpeedsNotNull_ShouldShouldAddScooterSpeed()
        {
            var orderCount = Context.ScooterSpeed.Count();

            await _testee.AddAsync(_newScooterSpeed);

            Context.ScooterSpeed.Count().Should().Be(orderCount + 1);
        }

        [Fact]
        public void GetAll_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _scooterSpeedContext.Set<ScooterSpeed>()).Throws<Exception>();

            _testeeFake.Invoking(x => x.GetAll()).Should().Throw<Exception>().WithMessage("Couldn't retrieve entities Exception of type 'System.Exception' was thrown.");
        }

       
        
    }
}