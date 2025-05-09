using Entities.Domain;
using Entities.DTO.WorkoutPlanDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Extensions
{
    public static class WorkoutPlanExtension
    {
        public static WorkoutPlanResponse ToWorkouPlanResponse(this WorkoutPlan workoutPlan)
        {
            return new WorkoutPlanResponse
            {
                Id = workoutPlan.WorkoutPlanId,
                Title = workoutPlan.Title,
                Description = workoutPlan.Description,
                TrainerId = workoutPlan.UserId,
                TrainerName = workoutPlan.Trainer?.User.DisplayName,
                WorkoutExercises = workoutPlan.WorkoutExercises?.Select(w => w.ToWorkoutExerciseResponse()).ToList(),
            };
        }
    }
}
