using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Domain
{
    public class WorkoutExercise
    {
        [Key]
        public int WorkoutExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int WorkoutPlanId { get; set; } // foreignkey
        public WorkoutPlan WorkoutPlan { get; set; } // one to one -> WorkoutPlan
    }
}
