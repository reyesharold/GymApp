using Entities.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.ClassDTO
{
    public class ClassAddRequest
    {
        [Required(ErrorMessage = "Class Name can't be blank")]
        public string ClassName {  get; set; }

        [Required(ErrorMessage = "Capacity can't be blank")]
        [Range(1,int.MaxValue, ErrorMessage = "Capacity is out of range")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Schedule can't be blank")]
        [DataType(DataType.DateTime)]
        public DateTime ScheduleDateTime { get; set; }

        [Required(ErrorMessage = "Trainer ID is required")]
        public Guid TrainerId { get; set; }

        public Class ToClass()
        {
            return new Class
            {
                ClassName = ClassName,
                Capacity = Capacity,
                ScheduleDateTime = ScheduleDateTime,
                TrainerId = TrainerId
            };
        }
    }
}
