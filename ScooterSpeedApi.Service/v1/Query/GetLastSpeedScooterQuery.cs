using MediatR;
using ScooterSpeedApi.Domain;

namespace ScooterSpeedApi.Service.v1.Query
{
    public class GetLastSpeedScooterQuery : IRequest<ScooterSpeed>
    {
        public int ScooterId { get; set; }
    }
}