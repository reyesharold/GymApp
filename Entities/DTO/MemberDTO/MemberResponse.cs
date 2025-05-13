using Entities.DTO.AttendanceDTO;
using Entities.DTO.BookingDTO;
using Entities.DTO.MembershipDTO;
using Entities.DTO.PaymentDTO;
using Entities.DTO.UserDTO;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.MemberDTO
{
    public class MemberResponse
    {
        public UserResponse User { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public int MembershipId { get; set; }
        public string MembershipName { get; set; }
        public decimal MembershipPrice { get; set; }
        public ICollection<PaymentResponse> Payments { get; set; }
        public ICollection<AttendanceResponse> Attendances { get; set; }
        public ICollection<BookingResponse> Bookings { get; set; }
    }
}
