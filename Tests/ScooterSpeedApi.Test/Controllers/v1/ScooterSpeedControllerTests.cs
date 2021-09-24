using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ScooterSpeedApi.Controllers.v1;
using ScooterSpeedApi.Domain;
using ScooterSpeedApi.Service.v1.Command;
using ScooterSpeedApi.Service.v1.Query;
using Xunit;

namespace ScooterSpeedApi.Test.Controllers.v1
{
    public class ScooterSpeedControllerTests
    {
        private readonly ScooterSpeedController _testee;
        private readonly IMediator _mediator;

        public ScooterSpeedControllerTests()
        {
            _mediator = A.Fake<IMediator>();
            _testee = new ScooterSpeedController(A.Fake<IMapper>(), _mediator);

            var scooterSpeeds = new List<ScooterSpeed>
            {
                new()
                {
                    Id = 1,
                    Speed = 55,
                    Time = DateTime.Now,
                    CoordinateX = 60,
                    CoordinateY = 40,
                    ScooterId = 5
                },
                new()
                {
                    Id = 2,
                    Speed = 60,
                    Time = DateTime.Now,
                    CoordinateX = 60,
                    CoordinateY = 40,
                    ScooterId = 6
                }
            };
            
            A.CallTo(() => _mediator.Send(A<CreateScooterSpeedCommand>._, A<CancellationToken>._)).Returns(scooterSpeeds.First());
            A.CallTo(() => _mediator.Send(A<GetSpeedsAllScootersQuery>._, A<CancellationToken>._)).Returns(scooterSpeeds);
        }
        
        [Fact]
        public async void ScooterSpeds_ShouldReturnListOfSpeeds()
        {
            var result = await _testee.Speeds();

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<List<ScooterSpeed>>();
            result.Value.Count.Should().Be(2);
        }

        [Fact]
        public async void Orders_WhenNoOrdersWereFound_ShouldReturnEmptyList()
        {
            A.CallTo(() => _mediator.Send(A<GetSpeedsAllScootersQuery>._, A<CancellationToken>._)).Returns(new List<ScooterSpeed>());

            var result = await _testee.Speeds();

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<List<ScooterSpeed>>();
            result.Value.Count.Should().Be(0);
        }
    }
}