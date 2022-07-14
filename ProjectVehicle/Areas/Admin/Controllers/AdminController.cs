using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectVehicle.Data;
using ProjectVehicle.Models;

namespace ProjectVehicle.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ProjectVehicleContext _context;

        public AdminController(ProjectVehicleContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "Abrv" : "";
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "Model" : "";
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "Country" : "";
            ViewData["CurrentSort"] = sortOrder;

            var vehicles = from v in _context.VehicleMake select v;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                vehicles = vehicles.Where(v => v.Name.Contains(searchString) ||
                                               v.Abrv.Contains(searchString) ||
                                               v.Model.Contains(searchString) ||
                                               v.Country.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nameDesc":
                    vehicles = vehicles.OrderByDescending(v => v.Name);
                    break;
                case "Abrv":
                    vehicles = vehicles.OrderByDescending(v => v.Abrv);
                    break;
                case "Model":
                    vehicles = vehicles.OrderByDescending(v => v.Model);
                    break;
                case "Country":
                    vehicles = vehicles.OrderByDescending(v => v.Country);
                    break;
                default:
                    vehicles = vehicles.OrderBy(v => v.Name);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<VehicleMake>.CreateAsync(vehicles.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

    }
}
