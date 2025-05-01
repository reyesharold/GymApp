using Entities.Domain;
using Entities.DTO.TrainerDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Extensions
{
    public static class TrainerExtension
    {
        public static TrainerResponse ToTrainerResponse(this Trainer trainer)
        {
            return new TrainerResponse
            {
                User = trainer.User.ToUserResponse(),
                Specialties = trainer.Specialties,
                Certifications = trainer.Certifications,
                WorkoutPlans = trainer.WorkoutPlans.Select(w => w.ToWorkouPlanResponse()).ToList(),
                Classes = trainer.Classes.Select(c => c.ToClassReponse()).ToList(),
            };
        }
    }
}
