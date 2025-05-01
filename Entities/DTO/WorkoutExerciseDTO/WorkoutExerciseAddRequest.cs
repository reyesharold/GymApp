using Entities.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.WorkoutExerciseDTO
{
    public class WorkoutExerciseAddRequest
    {
        [Required(ErrorMessage = "Exercise name can't be blank")]
        public string ExerciseName { get; set; }

        [Required(ErrorMessage = "Sets can't be blank")]
        [Range(1,100, ErrorMessage = "Sets is out of Range")]
        public int Sets { get; set; }

        [Required(ErrorMessage = "Reps can't be blank")]
        [Range(1, 100, ErrorMessage = "Reps is out of Range")]
        public int Reps { get; set; }

        [Required(ErrorMessage = "WorkoutPlanId is required")]
        public int? WorkoutPlanId { get; set; }

        public WorkoutExercise ToWorkoutExercise()
        {
            return new WorkoutExercise
            {
                ExerciseName = ExerciseName,
                Sets = Sets,
                Reps = Reps,
                WorkoutPlanId = WorkoutPlanId
            };
        }
    }
}
