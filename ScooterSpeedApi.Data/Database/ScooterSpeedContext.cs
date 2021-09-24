using Microsoft.EntityFrameworkCore;
using ScooterSpeedApi.Domain;

namespace ScooterSpeedApi.Data.Database
{
    public class ScooterSpeedContext : DbContext
    {
        public ScooterSpeedContext()
        {




        }

        public ScooterSpeedContext(DbContextOptions<ScooterSpeedContext> options)
            : base(options)
        {
            //var orders = new[]
            //{
            //    new Order
            //    {
            //        Id = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
            //        OrderState = 1,
            //        CustomerGuid = Guid.Parse("d3e3137e-ccc9-488c-9e89-50ba354738c2"),
            //        CustomerFullName = "Wolfgang Ofner"
            //    },
            //    new Order
            //    {
            //        Id = Guid.Parse("bffcf83a-0224-4a7c-a278-5aae00a02c1e"),
            //        OrderState = 1,
            //        CustomerGuid = Guid.Parse("4a2f1e35-f527-4136-8b12-138a57e1ba08"),
            //        CustomerFullName = "Darth Vader"
            //    },
            //    new Order
            //    {
            //        Id = Guid.Parse("58e5cd7d-856b-4224-bdff-bd8f85bf5a6d"),
            //        OrderState = 2,
            //        CustomerGuid = Guid.Parse("334feb16-d7bb-4ca9-ab56-f4fadeb88d21"),
            //        CustomerFullName = "Son Goku"
            //    }
            //};

            //Order.AddRange(orders);
            //SaveChanges();
        }

        public virtual DbSet<ScooterSpeed> ScooterSpeed { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ScooterSpeed>(entity =>
            {
                entity.Property(e => e.Speed).IsRequired();
                entity.Property(e => e.Time).IsRequired();
                entity.Property(e => e.CoordinateX).IsRequired();
                entity.Property(e => e.CoordinateY).IsRequired();
                entity.Property(e => e.ScooterId).IsRequired();
            });
        }
    }
}