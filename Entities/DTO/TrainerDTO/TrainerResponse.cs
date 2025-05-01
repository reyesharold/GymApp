using Entities.DTO.ClassDTO;
using Entities.DTO.UserDTO;
using Entities.DTO.WorkoutPlanDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.TrainerDTO
{
    public class TrainerResponse
    {
        public UserResponse User { get; set; }
        public string Specialties { get; set; }
        public string Certifications { get; set; }
        public ICollection<WorkoutPlanResponse> WorkoutPlans { get; set; }
        public ICollection<ClassResponse> Classes { get; set; }
    }
}
