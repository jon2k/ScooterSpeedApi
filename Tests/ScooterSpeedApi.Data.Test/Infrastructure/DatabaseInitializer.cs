using System;
using System.Linq;
using ScooterSpeedApi.Data.Database;
using ScooterSpeedApi.Domain;

namespace ScooterSpeedApi.Data.Test.Infrastructure
{
    public class DatabaseInitializer
    {
        public static void Initialize(ScooterSpeedContext context)
        {
            if (context.ScooterSpeed.Any())
            {
                return;
            }

            Seed(context);
        }

        private static void Seed(ScooterSpeedContext context)
        {
            var scooterSpeeds = new[]
            {
                new ScooterSpeed
                {
                    Id = Guid.NewGuid(),
                    Speed = 20,
                    Time = DateTime.Now,
                    CoordinateX = 60,
                    CoordinateY = 60,
                    ScooterId = 1
                },
                new ScooterSpeed
                {
                    Id = Guid.NewGuid(),
                    Speed = 25,
                    Time = DateTime.Now,
                    CoordinateX = 60,
                    CoordinateY = 60,
                    ScooterId = 2
                },
                new ScooterSpeed
                {
                    Id = Guid.NewGuid(),
                    Speed = 30,
                    Time = DateTime.Now,
                    CoordinateX = 60,
                    CoordinateY = 60,
                    ScooterId = 3
                }
            };

            context.ScooterSpeed.AddRange(scooterSpeeds);
            context.SaveChanges();
        }
    }
}