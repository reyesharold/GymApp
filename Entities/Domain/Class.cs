using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Domain
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int Capacity { get; set; }
        public Guid TrainerId { get; set; } // foreignKey for Trainer
        public Trainer Trainer { get; set; } // one to one -> Trainer
        public DateTime ScheduleDateTime { get; set; }
        public ICollection<Booking> Bookings { get; set; } // one to many ->  Booking
    }
}
