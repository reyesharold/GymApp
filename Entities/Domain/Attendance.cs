using Entities.Identities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Domain
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public Guid UserId { get; set; } // foreign key for Member
        public Member Member {  get; set; } // one to one -> Member
    }
}