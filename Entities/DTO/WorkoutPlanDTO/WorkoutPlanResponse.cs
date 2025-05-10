using Entities.Domain;
using Entities.DTO.WorkoutExerciseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.WorkoutPlanDTO
{
    public class WorkoutPlanResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid TrainerId { get; set; }
        public string? TrainerName { get; set; }
        public ICollection<WorkoutExerciseResponse>? WorkoutExercises { get; set; }
    }
}
