using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreCodeCamp.Data;
using CoreCodeCamp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CoreCodeCamp.Controllers
{
    //[controller] says whatever comes before the word controller is the route to be used
    [Route("api/[controller]")]
    [ApiController] //Now Controller will automagically attempt to do body binding
    public class CampsController : ControllerBase
    {
        private readonly ICampRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public CampsController(ICampRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        //actions are the endpoint of requests
        //[HttpGet]
        //public async Task<IActionResult> GetCamps()
        //{
        //    try 
        //    {
        //        var results = await _repository.GetAllCampsAsync();

        //        // map a CampModel from a Camp individually, will also support all sorts of collections of those types as well.
        //        CampModel[] models = _mapper.Map<CampModel[]>(results);

        //        return Ok(models);
        //    }
        //    catch(Exception)
        //    {
        //        return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
        //    }

        //}

        /// <summary>
        /// Returns all camps from db; takes array of camps and returns array of models 
        /// </summary>

        [HttpGet]
        public async Task<ActionResult<CampModel[]>> GetCamps(bool includeTalks = false)
        {
            //with ActionResult<TValue> we can say which type of return value should be returned
            //if we're returning a type that matches this then it's going to return Ok for us automatically.
            try
            {
                var results = await _repository.GetAllCampsAsync(includeTalks);

                //can explicity return mapped results instead of setting to a variable
                return _mapper.Map<CampModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }
        
        /// <summary>
        /// returns single camp from db, takes individual camp and returns single camp model
        /// uses moniker as identifier, I.E. "ATL2018"
        /// </summary>
        
        [HttpGet("{moniker}")]
        public async Task<ActionResult<CampModel>> Get(string moniker)
        {
            try
            {
                var result = await _repository.GetCampAsync(moniker);

                if (result == null) return NotFound();

                return _mapper.Map<CampModel>(result);
            }
            catch(Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }

        [HttpGet("search")]
        public async Task<ActionResult<CampModel[]>> SearchByDate(DateTime theDate, bool includeTalks = false)
        {
            try
            {
                var results = await _repository.GetAllCampsByEventDate(theDate, includeTalks);

                if (!results.Any()) return NotFound();

                return _mapper.Map<CampModel[]>(results);
            }
            catch(Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        //Can also use ([FromBody]CampModel campModel) to tell controller to expect to retrieve the incoming CampModel from the body of the POST request.
        //Also note this will only bind data that it expects to get for a CampModel, so if a property that doesn't belong to CampModel is passed during the POST, that data will get lost or ignored. 

        public async Task<ActionResult<CampModel>> Post(CampModel campModel) 
        {
            try
            {
                //Test for existing moniker
                var existing = await _repository.GetCampAsync(campModel.Moniker);
                if (existing != null)
                    return BadRequest("Moniker in use");

                //Generate URI (link)
                var location = _linkGenerator.GetPathByAction("Get", "Camps", new { moniker = campModel.Moniker });

                if (string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Could not use current moniker");
                }

                //Create a new Camp
                var camp = _mapper.Map<Camp>(campModel); 
                _repository.Add(camp);


                if (await _repository.SaveChangesAsync())
                {
                    return Created(location, _mapper.Map<CampModel>(camp));
                }

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

    }
}
