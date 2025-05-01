using Entities.Domain;
using Entities.DTO.ClassDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Extensions
{
    public static class ClassExtension
    {
        public static ClassResponse ToClassReponse(this Class classEntity)
        {
            return new ClassResponse
            {
                Id = classEntity.ClassId,
                ClassName = classEntity.ClassName,
                Capacity = classEntity.Capacity,
                ScheduleDateTime = classEntity.ScheduleDateTime,
                TrainerId = classEntity.TrainerId,
                TrainerName = classEntity.Trainer.User.DisplayName,
                Bookings = classEntity.Bookings.Select(b => b.ToBookingReponse()).ToList(),
            };
        }
    }
}
