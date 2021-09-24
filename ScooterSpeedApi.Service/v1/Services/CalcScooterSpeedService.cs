using System;
using System.Diagnostics;
using MediatR;
using ScooterSpeedApi.Domain;
using ScooterSpeedApi.Service.v1.Command;
using ScooterSpeedApi.Service.v1.Models;
using ScooterSpeedApi.Service.v1.Query;

namespace ScooterSpeedApi.Service.v1.Services
{
    public class CalcScooterSpeedService : ICalcScooterSpeedService
    {
        private readonly IMediator _mediator;

        public CalcScooterSpeedService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async void CalcScooterSpeed(Scooter scooter)
        {
            Console.WriteLine("Added scooter");
            try
            {
                var lastDataFromScooter = await _mediator.Send(new GetLastSpeedScooterQuery()
                {
                    ScooterId = scooter.ScooterId
                }) ?? new ScooterSpeed()
                {
                    Id = scooter.Id,
                    Speed = 0,
                    Time = scooter.Time,
                    CoordinateX = scooter.CoordinateX,
                    CoordinateY = scooter.CoordinateY,
                    ScooterId = scooter.ScooterId
                };

                var currentSpeed = CalcSpeed(scooter.CoordinateX, lastDataFromScooter.CoordinateX,
                    scooter.CoordinateY, lastDataFromScooter.CoordinateY,
                    scooter.Time, lastDataFromScooter.Time);


                await _mediator.Send(new CreateScooterSpeedCommand()
                {
                    ScooterSpeed = new ScooterSpeed()
                    {
                        Speed = (float)currentSpeed,
                        Time = scooter.Time,
                        CoordinateX = scooter.CoordinateX,
                        CoordinateY = scooter.CoordinateY,
                        ScooterId = scooter.ScooterId
                    }
                });
            }
            catch (Exception ex)
            {
                // log an error message here

                Debug.WriteLine(ex.Message);
            }
        }

        private double CalcSpeed(double x_1, double x_2, double y_1, double y_2, DateTime time1, DateTime time2)
        {
            if (time1==time2)
            {
                return 0;
            }
            var x1 = CoordinateToDistance(x_1, true);
            var x2 = CoordinateToDistance(x_2, true);
            var y1 = CoordinateToDistance(y_1, false);
            var y2 = CoordinateToDistance(y_2, false);
            double katet1;
            double katet2;
            if (x1 >= 0)
            {
                if (x2 >= 0)
                    katet1 = Math.Abs(x1 - x2);
                else
                    katet1 = x1 + Math.Abs(x2);
            }
            else
            {
                if (x2 >= 0)
                    katet1 = x2 + Math.Abs(x1);
                else
                    katet1 = Math.Abs(x1 + x2);
            }

            if (y1 >= 0)
            {
                if (y2 >= 0)
                    katet2 = Math.Abs(y1 - y2);
                else
                    katet2 = y1 + Math.Abs(y2);
            }
            else
            {
                if (y2 >= 0)
                    katet2 = y2 + Math.Abs(y1);
                else
                    katet2 = Math.Abs(y1 + y2);
            }

            var gipotenuza = Math.Sqrt(Math.Pow(katet1, 2) + Math.Pow(katet2, 2));

            return gipotenuza / (time1 - time2).TotalHours;
        }

        private double CoordinateToDistance(double cordinate, bool IsShirota)
        {
            // converting coordinates to a distance from 0 shiroti and 0 dolgoti
            return cordinate;
        }
    }
}