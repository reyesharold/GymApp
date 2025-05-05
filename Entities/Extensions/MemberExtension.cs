using Entities.Domain;
using Entities.DTO.MemberDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Extensions
{
    public static class MemberExtension
    {
        public static MemberResponse ToMemberResponse(this Member member)
        {
            return new MemberResponse
            {
                User = member.User.ToUserResponse(),
                DateOfBirth = member.DateOfBirth,
                Gender = member.Gender,
                MembershipId = member.MembershipId,
                MembershipName = member.Membership.Name,
                MembershipPrice = member.Membership.Price,
                Payments = member.Payments.Select(p => p.ToPaymentResponse()).ToList(),
                Attendances = member.Attendances.Select(a => a.ToAttendaceResponse()).ToList(),
                Bookings = member.Bookings.Select(b => b.ToBookingReponse()).ToList(),
            };
        }
    }
}
