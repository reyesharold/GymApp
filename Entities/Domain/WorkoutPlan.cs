using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Domain
{
    public class WorkoutPlan
    {
        [Key]
        public int WorkoutPlanId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; } // foreignKey for Trainer
        public Trainer? Trainer { get; set; } // one to one -> Trainer
        public ICollection<WorkoutExercise>? WorkoutExercises { get; set; } // one to many -> WorkoutExercise
    }
}