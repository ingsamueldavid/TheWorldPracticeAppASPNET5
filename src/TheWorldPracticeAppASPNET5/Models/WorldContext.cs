using System;
using Microsoft.Data.Entity;

namespace TheWorldPracticeAppASPNET5.Models
{
    public class WorldContext:DbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }

        public WorldContext()
        {
            Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var connString = Startup.Configuration["Data:WorldContextConnection"];
            optionsBuilder.UseSqlServer(connString);






            base.OnConfiguring(optionsBuilder);
        }
    }



}
