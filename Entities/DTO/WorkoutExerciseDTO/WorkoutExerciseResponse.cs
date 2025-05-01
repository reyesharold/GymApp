using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.WorkoutExerciseDTO
{
    public class WorkoutExerciseResponse
    {
        public int Id { get; set; }
        public string ExerciseName { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int? WorkoutPlanId { get; set; }
        public string? WorkoutPlanName { get; set; }
    }
}
