using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ScooterSpeedApi.Data.Repository.v1;
using ScooterSpeedApi.Domain;

namespace ScooterSpeedApi.Service.v1.Query
{
    public class GetLastSpeedScooterQueryHandler : IRequestHandler<GetLastSpeedScooterQuery, ScooterSpeed>
    {
        private readonly IScooterSpeedRepository _scooterSpeedRepository;

        public GetLastSpeedScooterQueryHandler(IScooterSpeedRepository scooterSpeedRepository)
        {
            _scooterSpeedRepository = scooterSpeedRepository;
        }

        public async Task<ScooterSpeed> Handle(GetLastSpeedScooterQuery request, CancellationToken cancellationToken)
        {
            return await _scooterSpeedRepository.GetLastSpeedAsync(request.ScooterId, cancellationToken);
        }
    }
}