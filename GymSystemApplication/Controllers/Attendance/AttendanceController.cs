using Entities.DTO.AttendanceDTO;
using Entities.ErrorViewModelClass;
using Microsoft.AspNetCore.Mvc;
using Services.AttendanceServices;
using System.Threading.Tasks;

namespace GymSystemApplication.Controllers.Attendance
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [Route("Attendance/Check-In")]
        [HttpGet]
        public IActionResult CheckIn()
        {
            return View();
        }
        [Route("Attendance/Check-In")]
        [HttpPost]
        public async Task<IActionResult> CheckIn(AttendanceAddRequest request)
        {
            try
            {
                var response = await _attendanceService.CreateAttendanceAsync(request);

                return View(nameof(AttendanceDetails), response);
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

        [Route("Attendance/Details")]
        [HttpGet]
        public async Task<IActionResult> AttendanceDetails(AttendanceResponse attendance)
        {
            try
            {
                var details = await _attendanceService.GetAttendanceAsync(attendance.AttendanceId);

                return View(details);
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

        [Route("Attendance/Confirm/Check-Out")]
        [HttpGet]
        public IActionResult ConfirmCheckOut(int attendanceId)
        {
            try
            {
                ViewBag.AttendanceId = attendanceId;
                return View();
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
        [Route("Attendance/Check-Out")]
        [HttpPost]
        public async Task<IActionResult> CheckOut(int attendanceId)
        {
            try
            {
                var response = await _attendanceService.CheckOutAsync(attendanceId);

                return View(nameof(AttendanceDetails), response);
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

        [Route("Attendance/Member-check-ins")]
        [HttpGet]
        public async Task<IActionResult> DisplayAttendanceOfMembers(Guid memberId)
        {
            try
            {
                var records = await _attendanceService.GetAttendanceOfMemberAsync(memberId);

                return View(records);
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
    }
}
