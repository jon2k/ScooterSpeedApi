using System;
using Microsoft.EntityFrameworkCore;
using ScooterSpeedApi.Data.Database;

namespace ScooterSpeedApi.Data.Test.Infrastructure
{
    public class DatabaseTestBase : IDisposable
    {
        protected readonly ScooterSpeedContext Context;

        public DatabaseTestBase()
        {
            var options = new DbContextOptionsBuilder<ScooterSpeedContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            Context = new ScooterSpeedContext(options);

            Context.Database.EnsureCreated();

            DatabaseInitializer.Initialize(Context);
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();

            Context.Dispose();
        }
    }
}