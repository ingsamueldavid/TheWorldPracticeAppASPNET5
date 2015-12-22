
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace TheWorldPracticeAppASPNET5.Models
{//aqui un comentario salvaje
    public class WorldUser : IdentityUser
    {
        public DateTime FirstTrip { get; set; }

    }
}