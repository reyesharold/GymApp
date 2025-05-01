using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Domain;

namespace Entities.DTO.WorkoutPlanDTO
{
    public class WorkoutPlanAddRequest
    {
        [Required(ErrorMessage = "Title can't be blank")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description can't be blank")]
        public string Description { get; set; }

        [Required(ErrorMessage = "UserId can't be blank")]
        public Guid UserId { get; set; } // trainerID

        public WorkoutPlan ToWorkoutPlan()
        {
            return new WorkoutPlan
            {
                Title = Title,
                Description = Description,
                UserId = UserId
            };
        }
    }

}
