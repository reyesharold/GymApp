using Entities.Domain;
using Entities.DTO.WorkoutExerciseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Extensions
{
    public static class WorkoutExerciseExtension
    {
        public static WorkoutExerciseResponse ToWorkoutExerciseResponse(this WorkoutExercise workoutExercise)
        {
            return new WorkoutExerciseResponse
            {
                Id = workoutExercise.WorkoutExerciseId,
                ExerciseName = workoutExercise.ExerciseName,
                Sets = workoutExercise.Sets,
                Reps = workoutExercise.Reps,
                WorkoutPlanId = workoutExercise.WorkoutPlanId,
                WorkoutPlanName = workoutExercise.WorkoutPlan?.Title
            };
        }
    }
}
