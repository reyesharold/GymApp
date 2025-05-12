using Microsoft.AspNetCore.Mvc;

namespace GymSystemApplication.Controllers.Booking
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
