using MediatR;
using ScooterSpeedApi.Domain;

namespace ScooterSpeedApi.Service.v1.Command
{
    public class CreateScooterSpeedCommand : IRequest<ScooterSpeed>
    {
        public ScooterSpeed ScooterSpeed { get; set; }
    }
}
