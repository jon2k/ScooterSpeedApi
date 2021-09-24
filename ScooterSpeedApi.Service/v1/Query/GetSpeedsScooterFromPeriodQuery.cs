using System;
using System.Collections.Generic;
using MediatR;
using ScooterSpeedApi.Domain;

namespace ScooterSpeedApi.Service.v1.Query
{
   public class GetSpeedsScooterFromPeriodQuery : IRequest<List<ScooterSpeed>>
    {
        public int ScooterId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
