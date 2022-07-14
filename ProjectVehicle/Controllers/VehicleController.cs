using Microsoft.AspNetCore.Mvc;

namespace ProjectVehicle.Controllers
{
    public class VehicleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
