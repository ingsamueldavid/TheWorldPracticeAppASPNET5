using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TheWorldPracticeAppASPNET5.Models;
using TheWorldPracticeAppASPNET5.ViewModels;

namespace TheWorldPracticeAppASPNET5.Controllers
{
    [Authorize]
    [Route("api/trips")]
    public class TripController : Controller
    {
        private ILogger<TripController> _logger;
        private IWorldRepository _repository;
        public TripController(IWorldRepository repository,ILogger<TripController> logger)
        {
            try
            {
              
                _repository = repository;
                _logger = logger;
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet("")]
        public JsonResult Get()
        {


            var trips = _repository.GetUserTripsWithStops(User.Identity.Name);
            var results = AutoMapper.Mapper.Map<IEnumerable<TripViewModel>>(trips);

  
            return Json(results);
        }
        [HttpPost("")]
        public JsonResult Post([FromBody]TripViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var newTrip = AutoMapper.Mapper.Map<Trip>(vm);
                    newTrip.UserName = User.Identity.Name;
                    //save to the database

                    _logger.LogInformation("Attempting to save a new trip");
                    _repository.AddTrip(newTrip);
                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(AutoMapper.Mapper.Map<TripViewModel>(newTrip));


                    }
                   

                }
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to save new trip", e);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

              


                return Json(new { Message = e.Message });
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new {Message="Failed", ModelState = ModelState });
        }


    }
}
