using System.Collections.Generic;

namespace TheWorldPracticeAppASPNET5.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();

        IEnumerable<Trip> GetAllTripsWithStops();
        void AddTrip(Trip trip);
        bool SaveAll();
        Trip GetTripByName(string tripName);
        void AddStop(string tripName, Stop newStop);
    }
}