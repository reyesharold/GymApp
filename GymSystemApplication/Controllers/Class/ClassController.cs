using Entities.DTO.ClassDTO;
using Entities.ErrorViewModelClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.ClassServices;
using Services.TrainerServices;
using System.Threading.Tasks;

namespace GymSystemApplication.Controllers.Class
{
    public class ClassController : Controller
    {
        private readonly IClassService _classService;
        private readonly ITrainerService _trainerService;

        public ClassController(IClassService classService, ITrainerService trainerService)
        {
            _classService = classService;
            _trainerService = trainerService;
        }

        [Route("Class/Create")]
        [HttpGet]
        public async Task<IActionResult> CreateClass()
        {
            try
            {
                var trainers = await _trainerService.GetAllAsync();

                ViewBag.Trainers = trainers.Select(temp => new SelectListItem
                {
                    Value = temp.User.Id.ToString(),
                    Text = temp.User.DisplayName,
                });

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
        [Route("Class/Create")]
        [HttpPost]
        public async Task<IActionResult> CreateClass(ClassAddRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Errors = ModelState.Values.SelectMany(e => e.Errors).Select(d => d.ErrorMessage);
                    return View(request);
                }

                var response = await _classService.AddClassAsync(request);

                return View("DisplayClassDetails", response);
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

        [Route("Class/DisplayDetails")]
        [HttpGet]
        public async Task<IActionResult> DisplayClassDetails(ClassResponse request)
        {
            try
            {
                var response = await _classService.GetClassViaIdAsync(request.Id);

                return View(response);
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

        [Route("Class/Display-All")]
        [HttpGet]
        public async Task<IActionResult> DisplayAllClasses()
        {
            try
            {
                var classes = await _classService.GetAllClassesAsync();

                return View(classes);
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
