using Entities.DTO.BookingDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.ClassDTO
{
    public class ClassResponse
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public int Capacity { get; set; }
        public DateTime ScheduleDateTime { get; set; }
        public Guid TrainerId { get; set; }
        public string TrainerName { get; set; }
        public ICollection<BookingResponse> Bookings { get; set; }
    }
} 