using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ScooterSpeedApi.Data.Repository.v1;
using ScooterSpeedApi.Domain;

namespace ScooterSpeedApi.Service.v1.Query
{
    public class GetSpeedsScooterFromPeriodQueryHandler : IRequestHandler<GetSpeedsScooterFromPeriodQuery, List<ScooterSpeed>>
    {
        private readonly IScooterSpeedRepository _scooterSpeedRepository;

        public GetSpeedsScooterFromPeriodQueryHandler(IScooterSpeedRepository scooterSpeedRepository)
        {
            _scooterSpeedRepository = scooterSpeedRepository;
        }

        public async Task<List<ScooterSpeed>> Handle(GetSpeedsScooterFromPeriodQuery request, CancellationToken cancellationToken)
        {
            return await _scooterSpeedRepository.GetSpeedsFromPreiodAsync(request.ScooterId, request.From, request.To, cancellationToken);
        }
    }
}