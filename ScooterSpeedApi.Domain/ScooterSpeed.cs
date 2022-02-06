using System;

namespace ScooterSpeedApi.Domain
{
    public class ScooterSpeed
    {
        public Guid Id { get; set; }
        public int ScooterId { get; set; }
        public double CoordinateX { get; set; }
        public double CoordinateY { get; set; }
        public DateTime Time { get; set; }
        public float Speed { get; set; }
    }
}
