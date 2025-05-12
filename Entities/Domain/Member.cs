using Entities.Enums;
using Entities.Identities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Domain
{
    public class Member
    {
        [Key]
        public Guid UserId { get; set; } // foreignKey & acts as MemberId
        public UserApplication User {  get; set; } // one to one -> UserApplication
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public int MembershipId { get; set; } // foreignKey
        public Membership Membership { get; set; } // one to one -> Membership
        public ICollection<Payment>? Payments { get; set; } // one to many -> Payment
        public ICollection<Attendance>? Attendances { get; set; } // one to many -> Attendance
        public ICollection<Booking>? Bookings { get; set; }// one to many -> Booking
    }
}
