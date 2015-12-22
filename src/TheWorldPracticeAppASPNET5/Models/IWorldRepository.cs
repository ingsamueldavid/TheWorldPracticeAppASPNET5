using System.Collections.Generic;

namespace TheWorldPracticeAppASPNET5.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();

        IEnumerable<Trip> GetAllTripsWithStops();
        void AddTrip(Trip trip);
        bool SaveAll();
        Trip GetTripByName(string tripName,string username);
        void AddStop(string tripName, Stop newStop ,string username);
        IEnumerable<Trip> GetUserTripsWithStops(string name);
    }
}