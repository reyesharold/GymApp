using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.AttendanceDTO
{
    public class AttendanceResponse
    {
        public int AttendanceId { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public Guid? MemberId { get; set; }
        public string? MemberName { get; set; }
    }
}
