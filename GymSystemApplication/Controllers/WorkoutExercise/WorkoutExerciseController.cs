using Entities.DTO.WorkoutExerciseDTO;
using Entities.ErrorViewModelClass;
using GymSystemApplication.Controllers.WorkoutPlan;
using Microsoft.AspNetCore.Mvc;
using Services.WorkoutExerciseServices;
using Services.WorkoutPlanServices;

namespace GymSystemApplication.Controllers.WorkoutExercise
{
    public class WorkoutExerciseController : Controller
    {
        private readonly IWorkoutExerciseService _workoutExerciseService;
        private readonly IWorkoutPlanService _workoutPlanService;

        public WorkoutExerciseController(IWorkoutExerciseService workoutExerciseService,IWorkoutPlanService workoutPlanService)
        {
            _workoutExerciseService = workoutExerciseService;
            _workoutPlanService = workoutPlanService;
        }


        [Route("WorkoutExercise/Create")]
        [HttpGet]
        public IActionResult AddWorkoutExercise(int WorkoutPlanId)
        {
            try
            {
                ViewBag.WorkoutPlanId = WorkoutPlanId;

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

        [Route("WorkoutExercise/Create")]
        [HttpPost]
        public async Task<IActionResult> AddWorkoutExercise(WorkoutExerciseAddRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Errors = ModelState.Values.SelectMany(e => e.Errors).Select(m => m.ErrorMessage);
                    return View(request);
                }

                var exerciseResponse = await _workoutExerciseService.CreateWorkoutExerciseAsync(request);

                var workoutPlan = await _workoutPlanService.GetWorkoutPlanViaIdAsync(exerciseResponse.WorkoutPlanId ?? 0);

                return RedirectToAction(nameof(WorkoutPlanController.WorkoutPlanDetails),"WorkoutPlan", workoutPlan);
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
