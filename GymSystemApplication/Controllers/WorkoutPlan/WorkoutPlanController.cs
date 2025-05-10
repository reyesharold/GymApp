using Entities.DTO.WorkoutPlanDTO;
using Entities.ErrorViewModelClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.TrainerServices;
using Services.WorkoutPlanServices;
using System.Threading.Tasks;

namespace GymSystemApplication.Controllers.WorkoutPlan
{
    public class WorkoutPlanController : Controller
    {
        private readonly IWorkoutPlanService _workoutPlanService;
        private readonly ITrainerService _trainerService;

        public WorkoutPlanController(IWorkoutPlanService workoutPlanService, ITrainerService trainerService)
        {
            _workoutPlanService = workoutPlanService;
            _trainerService = trainerService;
        }


        [Route("WorkoutPlan/Create")]
        [HttpGet]
        public async Task<IActionResult> CreateWorkoutPlan()
        {
            try
            {
                var trainers = await _trainerService.GetAllAsync();

                ViewBag.Trainers = trainers.Select(temp => new SelectListItem
                {
                    Value = temp.User.Id.ToString(),
                    Text = temp.User.DisplayName
                });

                return View();
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

        [Route("WorkoutPlan/Create")]
        [HttpPost]
        public async Task<IActionResult> CreateWorkoutPlan(WorkoutPlanAddRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Errors = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage);
                    return View(request);
                }

                var response = await _workoutPlanService.AddWorkoutPlanAsync(request);

                return View(nameof(WorkoutPlanDetails), response);
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

        [Route("WorkoutPlan/Display-Details")]
        [HttpGet]
        public async Task<IActionResult> WorkoutPlanDetails(WorkoutPlanResponse response)
        {
            try
            {
                var workoutPlan = new WorkoutPlanResponse();

                if(response.Id == 0) 
                { 
                    workoutPlan = await _workoutPlanService.GetWorkoutPlanViaTrainerIdAsync(response.TrainerId);
                    return View(workoutPlan);
                }

                workoutPlan = await _workoutPlanService.GetWorkoutPlanViaIdAsync(response.Id);

                return View(workoutPlan);
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

        [Route("WorkoutPlan/Display-All")]
        [HttpGet]
        public async Task<IActionResult> DisplayAllWorkoutPlans()
        {
            try
            {
                var workoutPlans = await _workoutPlanService.GetAllWorkoutPlanAsync();

                return View(workoutPlans);
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