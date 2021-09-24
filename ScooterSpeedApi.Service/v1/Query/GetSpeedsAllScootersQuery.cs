using System.Collections.Generic;
using MediatR;
using ScooterSpeedApi.Domain;

namespace ScooterSpeedApi.Service.v1.Query
{
    public class GetSpeedsAllScootersQuery : IRequest<List<ScooterSpeed>>
    {
   
    }
}