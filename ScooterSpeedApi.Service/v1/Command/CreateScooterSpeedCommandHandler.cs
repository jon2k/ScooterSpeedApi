using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ScooterSpeedApi.Data.Repository.v1;
using ScooterSpeedApi.Domain;

namespace ScooterSpeedApi.Service.v1.Command
{
    public class CreateScooterSpeedCommandHandler : IRequestHandler<CreateScooterSpeedCommand, ScooterSpeed>
    {
        private readonly IScooterSpeedRepository _scooterSpeedRepository;

        public CreateScooterSpeedCommandHandler(IScooterSpeedRepository scooterSpeedRepository)
        {
            _scooterSpeedRepository = scooterSpeedRepository;
        }

        public async Task<ScooterSpeed> Handle(CreateScooterSpeedCommand request, CancellationToken cancellationToken)
        {
            return await _scooterSpeedRepository.AddAsync(request.ScooterSpeed);
        }
    }
}