
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace TheWorldPracticeAppASPNET5.Models
{
    public class WorldUser : IdentityUser
    {
        public DateTime FirstTrip { get; set; }

    }
}