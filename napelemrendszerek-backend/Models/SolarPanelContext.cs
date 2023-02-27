using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using napelemrendszerek_backend.Models;

namespace SolarPanelSystem_backend.Models
{
    class SolarPanelContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartProjectConnection> PartProjectConnections { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectStates> ProjectStates { get; set; }
        public DbSet<Compartment> Compartments { get; set; }
        public DbSet<PartStates> PartStates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Server=overkill.servegame.com;Database=SolarDB;User Id=SolarBackend;Password=V33I*$2#5Uzx;");
        }
    }
}
