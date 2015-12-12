using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorldPracticeAppASPNET5.Models
{
    public class WorldRepository : IWorldRepository
    {


        private WorldContext _context;
        private ILogger<WorldRepository> _logger;
        public WorldRepository(WorldContext context,ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IEnumerable<Trip> GetAllTrips()
        {

            try
            {
               // throw new Exception("Errorsillo aqui");
                return _context.Trips.OrderBy(trip => trip.Name).ToList();
            }
            catch (Exception ex)
            {


                _logger.LogError("Could not get trips from db", ex);
                return null;
            }

        }

        public IEnumerable<Trip> GetAllTripsWithStops()
        {

            try
            {
               
                return _context.Trips.
                Include(trip => trip.Stops)
                .OrderBy(trip => trip.Name).ToList();
            }
            catch (Exception ex)
            {

                _logger.LogError("Could not get stops from db", ex);
                return null;
            }

           


        }


    }
}