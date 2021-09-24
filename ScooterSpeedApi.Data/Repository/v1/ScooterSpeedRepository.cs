using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScooterSpeedApi.Data.Database;
using ScooterSpeedApi.Domain;

namespace ScooterSpeedApi.Data.Repository.v1
{
    public class ScooterSpeedRepository : Repository<ScooterSpeed>, IScooterSpeedRepository
    {
        public ScooterSpeedRepository(ScooterSpeedContext scooterSpeedContext) : base(scooterSpeedContext)
        {
        }

        public async Task<List<ScooterSpeed>> GetSpeedsFromPreiodAsync(int scooterId, DateTime from, DateTime to, CancellationToken cancellationToken)
        {
            return await ScooterSpeedContext.ScooterSpeed.Where(x => x.Time>from && x.Time<to && x.ScooterId==scooterId)
                .ToListAsync( cancellationToken);
        }

        public async Task<ScooterSpeed> GetLastSpeedAsync(int scooterId, CancellationToken cancellationToken)
        {
            return await ScooterSpeedContext.ScooterSpeed.OrderByDescending(x => x.Time).FirstOrDefaultAsync(cancellationToken);
        }
    }
}