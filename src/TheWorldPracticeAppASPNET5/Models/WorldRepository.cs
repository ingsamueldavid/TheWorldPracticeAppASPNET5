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

        public void AddStop(string tripName,Stop newStop)
        {
           var theTrip = GetTripByName(tripName);
            newStop.Order = theTrip.Stops.Max(s => s.Order) + 1;
            theTrip.Stops.Add(newStop);
            _context.Stops.Add(newStop);
        }

        public void AddTrip(Trip trip)
        {

            _context.Add(trip);
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

        public Trip GetTripByName(string tripName)
        {
            return _context.Trips.Include(t => t.Stops).Where(trip => trip.Name == tripName).FirstOrDefault();
        }

        public bool SaveAll()
        {

           //if rows affected > 0 return true

           return _context.SaveChanges() > 0;
        }
    }
}