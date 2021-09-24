using ScooterSpeedApi.Service.v1.Models;

namespace ScooterSpeedApi.Service.v1.Services
{
    public interface ICalcScooterSpeedService
    {
        void CalcScooterSpeed(Scooter scooter);
    }
}