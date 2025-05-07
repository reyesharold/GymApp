using Entities.Domain;
using Entities.DTO.MemberDTO;
using Entities.Enums;
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

            return response.ToMemberResponse();
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

            return member.ToMemberResponse();
        }

        public async Task<ICollection<MemberResponse>> GetAllMembersAsync()
        {

            var members = await _commonRepo.GetAllAsync(m => 
            !m.User.UserRoles.Any(r => r.Role.Name == Roles.Trainer.ToString() || 
            !m.User.UserRoles.Any(r => r.Role.Name == Roles.Admin.ToString())), 
            
            query => query
            .Include(u => u.User)
                .ThenInclude(ur => ur.UserRoles)
                    .ThenInclude(r => r.Role)
            .Include(p => p.Payments)
            .Include(a => a.Attendances)
            .Include(b => b.Bookings)
            .Include(m => m.Membership)
            );

            return members.Select(temp => temp.ToMemberResponse()).ToList();
        }
    }
}
