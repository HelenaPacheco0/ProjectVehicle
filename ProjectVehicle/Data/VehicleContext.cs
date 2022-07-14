using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectVehicle.Models;

namespace ProjectVehicle.Models
{
    public class VehicleContext : DbContext
    {
        public VehicleContext() : base()
        {

        }

        public VehicleContext (DbContextOptions<VehicleContext> options)
            : base(options)
        {
        }

        public DbSet<ProjectVehicle.Models.VehicleMake> VehicleMake { get; set; } = default!;
    }
}
