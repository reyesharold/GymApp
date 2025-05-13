using Entities.DTO.BookingDTO;
using Entities.DTO.ClassDTO;
using Entities.ErrorViewModelClass;
using GymSystemApplication.Controllers.Class;
using Microsoft.AspNetCore.Mvc;
using Services.BookingServices;
using System.Threading.Tasks;

namespace GymSystemApplication.Controllers.Booking
{
    public class BookingController : Controller
    {
        private readonly IBookingServices _bookingServices;

        public BookingController(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
        }

        [Route("Booking/Confirmation")]
        [HttpGet]
        public IActionResult ConfirmBooking(int ClassId)
        {
            ViewBag.ClassId = ClassId;
            return View();
        }
        [Route("Booking/Create")]
        [HttpPost]
        public async Task<IActionResult> BookClass(BookingAddRequest request)
        {
            try
            {
                var response = await _bookingServices.AddBookingAsync(request);

                var classResponse = new ClassResponse
                {
                    Id = response.ClassId,
                };
                return RedirectToAction(nameof(ClassController.DisplayClassDetails),"Class",classResponse);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                };
                return View("Error", errorModel);
            }
        }

        [Route("Booking/Member-Booking")]
        [HttpGet]
        public async Task<IActionResult> DisplayBookingsOfMember(Guid MemberId)
        {
            try
            {
                var bookings = await _bookingServices.GetBookingsOfMemberAsync(MemberId);

                return View(bookings);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel
                {
                    Message = ex.InnerException?.Message ?? ex.Message
                };
                return View("Error", errorModel);
            }
        }
    }
}
