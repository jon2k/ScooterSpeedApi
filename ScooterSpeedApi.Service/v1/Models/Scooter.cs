using System;

namespace ScooterSpeedApi.Service.v1.Models
{
    public class Scooter
    {
        public int Id { get; set; }
        public int ScooterId { get; set; }
        public bool InUse { get; set; }
        public byte ChargePercent { get; set; }
        public double CoordinateX { get; set; }
        public double CoordinateY { get; set; }
        public DateTime Time { get; set; }
    }
}