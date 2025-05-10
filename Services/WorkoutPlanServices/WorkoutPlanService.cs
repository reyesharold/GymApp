using Entities.Domain;
using Entities.DTO.WorkoutPlanDTO;
using Entities.Extensions;
using Microsoft.EntityFrameworkCore;
using Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WorkoutPlanServices
{
    public class WorkoutPlanService : IWorkoutPlanService
    {
        private readonly ICommonRepo<WorkoutPlan> _commonRepo;

        public WorkoutPlanService(ICommonRepo<WorkoutPlan> commonRepo)
        {
            _commonRepo = commonRepo;
        }

        public async Task<WorkoutPlanResponse> AddWorkoutPlanAsync(WorkoutPlanAddRequest request)
        {
            var plan = await _commonRepo.AddAync(request.ToWorkoutPlan());

            var response = await _commonRepo.GetAsync(w => w.WorkoutPlanId == plan.WorkoutPlanId, query => query
            .Include(t => t.Trainer)
                .ThenInclude(u => u.User)
            .Include(w => w.WorkoutExercises)
            );

            return new WorkoutPlanResponse
            {
                Id = response.WorkoutPlanId,
                Title = response.Title,
                Description = response.Description,
                TrainerId = response.Trainer.UserId,
                TrainerName = response.Trainer?.User.DisplayName
            };
        }

        public async Task<ICollection<WorkoutPlanResponse>> GetAllWorkoutPlanAsync()
        {
            var workoutPlans = await _commonRepo.GetAllAsync(null, query => query
            .Include(t => t.Trainer)
                .ThenInclude(u => u.User)
            .Include(w => w.WorkoutExercises)
            );

            return workoutPlans.Select(temp => temp.ToWorkouPlanResponse()).ToList();
        }

        public async Task<WorkoutPlanResponse> GetWorkoutPlanViaIdAsync(int id)
        {
            var response = await _commonRepo.GetAsync(w => w.WorkoutPlanId == id, query => query
            .Include(t => t.Trainer)
                .ThenInclude(u => u.User)
            .Include(w => w.WorkoutExercises));

            if (response == null) { throw new ArgumentException("Invalid Workout Plan Id",nameof(id)); }

            return new WorkoutPlanResponse
            {
                Id = response.WorkoutPlanId,
                Title = response.Title,
                Description= response.Description,
                TrainerId= response.Trainer.UserId,
                TrainerName= response.Trainer?.User.DisplayName,
                WorkoutExercises = response.WorkoutExercises?.Select(temp => temp.ToWorkoutExerciseResponse()).ToList()
            };
        }

        public async Task<WorkoutPlanResponse> GetWorkoutPlanViaTrainerIdAsync(Guid id)
        {
            var response = await _commonRepo.GetAsync(w => w.UserId == id, query => query
            .Include(t => t.Trainer)
                .ThenInclude(u => u.User)
            .Include(w => w.WorkoutExercises));

            if (response == null) { throw new ArgumentException("Invalid Workout Plan Id", nameof(id)); }

            return new WorkoutPlanResponse
            {
                Id = response.WorkoutPlanId,
                Title = response.Title,
                Description = response.Description,
                TrainerId = response.Trainer.UserId,
                TrainerName = response.Trainer?.User.DisplayName,
                WorkoutExercises = response.WorkoutExercises?.Select(temp => temp.ToWorkoutExerciseResponse()).ToList()
            };

        }
    }
}
