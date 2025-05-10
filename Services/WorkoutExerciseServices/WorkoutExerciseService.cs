using Entities.Domain;
using Entities.DTO.WorkoutExerciseDTO;
using Microsoft.EntityFrameworkCore;
using Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WorkoutExerciseServices
{
    public class WorkoutExerciseService : IWorkoutExerciseService
    {
        private readonly ICommonRepo<WorkoutExercise> _commonRepo;

        public WorkoutExerciseService(ICommonRepo<WorkoutExercise> commonRepo)
        {
            _commonRepo = commonRepo;
        }

        public async Task<WorkoutExerciseResponse> CreateWorkoutExerciseAsync(WorkoutExerciseAddRequest request)
        {
            var workoutExercise = await _commonRepo.AddAync(request.ToWorkoutExercise());

            var response = await _commonRepo.GetAsync(temp => temp.WorkoutPlanId == workoutExercise.WorkoutPlanId, query => query
            .Include(w => w.WorkoutPlan)
            );

            return new WorkoutExerciseResponse
            {
                Id = response.WorkoutExerciseId,
                ExerciseName = response.ExerciseName,
                Sets = response.Sets,
                Reps = response.Reps,
                WorkoutPlanId = response.WorkoutPlanId,
                WorkoutPlanName = response.WorkoutPlan?.Title
            };
        }
    }
}
