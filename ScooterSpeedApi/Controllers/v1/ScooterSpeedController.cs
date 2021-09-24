using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScooterSpeedApi.Domain;
using ScooterSpeedApi.Service.v1.Query;

namespace ScooterSpeedApi.Controllers.v1
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class ScooterSpeedController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ScooterSpeedController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

       

        /// <summary>
        ///     Action to retrieve all speed all scooters.
        /// </summary>
        /// <returns>Returns a list of all speed or an empty list</returns>
        /// <response code="200">Returned if the list of speed was retrieved</response>
        /// <response code="400">Returned if the speed could not be retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<List<ScooterSpeed>>> Speeds()
        {
            try
            {
                Console.WriteLine("request");
                return await _mediator.Send(new GetSpeedsAllScootersQuery());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        /// <summary>
        ///     Action to retrieve speed concrete scooter for a period.
        /// </summary>
        /// <returns>Returns a list of speed for a period or an empty list</returns>
        /// <response code="200">Returned if the list of speed was retrieved</response>
        /// <response code="400">Returned if the speed could not be retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id:int}/{from:datetime}/{to:datetime}")]
        public async Task<ActionResult<List<ScooterSpeed>>> SpeedsByScooterFromPeriod(int id, DateTime from, DateTime to)
        {
            try
            {
                return await _mediator.Send(new GetSpeedsScooterFromPeriodQuery()
                {
                    ScooterId = id,
                    From = from,
                    To = to
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
    }
}