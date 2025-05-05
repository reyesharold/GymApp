using Entities.Domain;
using Entities.DTO.MemberDTO;
using Entities.Extensions;
using Microsoft.EntityFrameworkCore;
using Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MemberServices
{
    public class MemberService : IMemberService
    {
        private readonly ICommonRepo<Member> _commonRepo;

        public MemberService(ICommonRepo<Member> commonRepo)
        {
            _commonRepo = commonRepo;
        }

        public async Task<MemberResponse> AddMemberAsync(MemberAddRequest request)
        {
            if (request == null) { throw new ArgumentNullException("Invalid Member", nameof(request)); }

            var response = await _commonRepo.AddAync(request.ToMember());

            return new MemberResponse
            {
                User = response.User.ToUserResponse(),
                DateOfBirth = response.DateOfBirth,
                Gender = response.Gender,
            };
        }

        public async Task<MemberResponse> GetMemberViaId(Guid id)
        {
            var member = await _commonRepo.GetAsync(m => m.User.Id == id, query => query
            .Include(u => u.User)
            .Include(p => p.Payments)
            .Include(a => a.Attendances)
            .Include(b => b.Bookings)
            .Include(m => m.Membership));

            if (member == null) { throw new ArgumentException("Invalid Member ID", nameof(id)); }

            return new MemberResponse
            {
                User = member.User.ToUserResponse(),
                DateOfBirth= member.DateOfBirth,
                Gender= member.Gender,
                MembershipId = member.MembershipId,
                MembershipName = member.Membership.Name,
                MembershipPrice = member.Membership.Price,
                Payments = member.Payments.Select(temp => temp.ToPaymentResponse()).ToList(),
                Attendances = member.Attendances.Select(temp => temp.ToAttendaceResponse()).ToList(),
                Bookings = member.Bookings.Select(temp => temp.ToBookingReponse()).ToList(),
            };
        }
    }
}
