using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectVehicle.Data;
using ProjectVehicle.Models;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ProjectVehicle.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly ProjectVehicleContext _context;
        public VehicleMakesController(ProjectVehicleContext context)
        {
            _context = context;
        }


        // GET: VehicleMakes
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

        // GET: VehicleMakes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VehicleMake == null)
            {
                return NotFound();
            }

            var vehicleMake = await _context.VehicleMake
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            return View(vehicleMake);
        }

        // GET: VehicleMakes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleMakes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Abrv,Model,Country")] VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicleMake);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleMake);
        }

        // GET: VehicleMakes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VehicleMake == null)
            {
                return NotFound();
            }

            var vehicleMake = await _context.VehicleMake.FindAsync(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }
            return View(vehicleMake);
        }

        // POST: VehicleMakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Abrv,Model,Country")] VehicleMake vehicleMake)
        {
            if (id != vehicleMake.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleMake);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleMakeExists(vehicleMake.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleMake);
        }

        // GET: VehicleMakes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VehicleMake == null)
            {
                return NotFound();
            }

            var vehicleMake = await _context.VehicleMake
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            return View(vehicleMake);
        }

        // POST: VehicleMakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VehicleMake == null)
            {
                return Problem("Entity set 'ProjectVehicleContext.VehicleMake'  is null.");
            }
            var vehicleMake = await _context.VehicleMake.FindAsync(id);
            if (vehicleMake != null)
            {
                _context.VehicleMake.Remove(vehicleMake);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleMakeExists(int id)
        {
            return (_context.VehicleMake?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}