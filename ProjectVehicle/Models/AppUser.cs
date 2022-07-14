using Microsoft.AspNetCore.Identity;

namespace ProjectVehicle.Models
{
    public class AppUser : IdentityUser
    {
        public string Occupation { get; set; }
    }
}
