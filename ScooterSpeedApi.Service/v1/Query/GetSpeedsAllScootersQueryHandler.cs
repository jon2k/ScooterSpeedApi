using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ScooterSpeedApi.Data.Repository.v1;
using ScooterSpeedApi.Domain;

namespace ScooterSpeedApi.Service.v1.Query
{
    public class GetSpeedsAllScootersQueryHandler : IRequestHandler<GetSpeedsAllScootersQuery, List<ScooterSpeed>>
    {
        private readonly IScooterSpeedRepository _scooterSpeedRepository;

        public GetSpeedsAllScootersQueryHandler(IScooterSpeedRepository scooterSpeedRepository)
        {
            _scooterSpeedRepository = scooterSpeedRepository;
        }

        public async Task<List<ScooterSpeed>> Handle(GetSpeedsAllScootersQuery request, CancellationToken cancellationToken)
        {
            return _scooterSpeedRepository.GetAll().ToList();
        }
    }
}