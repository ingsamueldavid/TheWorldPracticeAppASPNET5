using System.Collections.Generic;

namespace TheWorldPracticeAppASPNET5.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();

        IEnumerable<Trip> GetAllTripsWithStops();
    }
}