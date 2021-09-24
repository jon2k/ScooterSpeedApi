using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ScooterSpeedApi.Domain;

namespace ScooterSpeedApi.Data.Repository.v1
{
    public interface IScooterSpeedRepository: IRepository<ScooterSpeed>
    {
        Task<List<ScooterSpeed>> GetSpeedsFromPreiodAsync(int scooterId, DateTime from, DateTime to, CancellationToken cancellationToken);
        Task<ScooterSpeed> GetLastSpeedAsync(int scooterId, CancellationToken cancellationToken);
        
    }
}