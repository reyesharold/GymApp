using Entities.DTO.TrainerDTO;
using Entities.ErrorViewModelClass;
using Microsoft.AspNetCore.Mvc;
using Services.TrainerServices;
using System.Threading.Tasks;

namespace GymSystemApplication.Controllers.Trainer
{
    
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        [Route("Trainer/Create")]
        [HttpGet]
        public IActionResult AddTrainer()
        {
            return View();
        }
        [Route("Trainer/Create")]
        [HttpPost]
        public async Task<IActionResult> AddTrainer(CreateTrainerDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Errors = ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage);
                    return View(request);
                }

                await _trainerService.CreateTrainerAsync(request);

                return RedirectToAction(nameof(DisplayTrainers));
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

        [Route("Trainer/Display-All")]
        [HttpGet]
        public async Task<IActionResult> DisplayTrainers()
        {
            var trainers = await _trainerService.GetAllAsync(); 

            return View(trainers);
        }

    }
}
