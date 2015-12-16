using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TheWorldPracticeAppASPNET5.Models;
using TheWorldPracticeAppASPNET5.Services;
using TheWorldPracticeAppASPNET5.ViewModels;

namespace TheWorldPracticeAppASPNET5.Controllers
{[Route("api/trips/{tripName}/stops")]
    public class StopController:Controller
    {
        private CoordService _coordService;
        private ILogger<TripController> _logger;
        private IWorldRepository _repository;
        public StopController(IWorldRepository repository, ILogger<TripController> logger,CoordService coordService)
        {
            try
            {

                _repository = repository;
                _logger = logger;
                _coordService = coordService;
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public JsonResult Get(string tripName)
        {
            try
            {
                var results = _repository.GetTripByName(tripName);
                if(results == null)
                {

                    return Json(null);
                }

               
                return Json(AutoMapper.Mapper.Map<IEnumerable<StopViewModel>>(results.Stops.OrderBy(o=>o.Order)));
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get stops for trip {tripName}", e);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Error Ocurred finding trip name");

            }
        }


        [HttpPost]
        public async Task<JsonResult> Post(string tripName,[FromBody]StopViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //map to entity
                    var newStop = AutoMapper.Mapper.Map<Stop>(vm);

                    //look up geocoordinates
                    var coordResult =await _coordService.Lookup(newStop.Name);


                    if (!coordResult.Success)
                    {

                        Response.StatusCode = (int)HttpStatusCode.BadRequest;

                        return Json(coordResult.Message);
                    }

                    newStop.Longitude = coordResult.Longitude;
                    newStop.Latitude = coordResult.Latitude;
                    //save to db
                    _repository.AddStop(tripName,newStop);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int)(int)HttpStatusCode.Created;

                        return Json(AutoMapper.Mapper.Map<StopViewModel>(newStop));
                    }


                }


            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to save new stop", e);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Failed to save new stop");

            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Validation Failed on new stop");



        }




    }
}
