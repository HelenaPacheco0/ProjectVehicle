using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectVehicle.Models;

namespace ProjectVehicle.Data
{
    public class ProjectVehicleContext : IdentityDbContext<AppUser>
    {
        public ProjectVehicleContext(DbContextOptions<ProjectVehicleContext> options)
            : base(options)
        {
        }

        public DbSet<ProjectVehicle.Models.VehicleMake> VehicleMake { get; set; } = default!;

        public DbSet<ProjectVehicle.Models.VehicleModel>? VehicleModel { get; set; }
    }
}