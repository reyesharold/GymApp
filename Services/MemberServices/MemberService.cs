using Entities.Domain;
using Entities.DTO.MemberDTO;
using Entities.Enums;
using Entities.Extensions;
using Microsoft.EntityFrameworkCore;
using Repositories.Common;
using Services.UserServices;
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

        /// <summary>
        /// Adds a member
        /// </summary>
        /// <param name="request"></param>
        /// <returns>MemberResponse</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<MemberResponse> AddMemberAsync(MemberAddRequest request)
        {
            if (request == null) { throw new ArgumentNullException("Invalid Member", nameof(request)); }

            var response = await _commonRepo.AddAync(request.ToMember());

            return response.ToMemberResponse();
        }

        /// <summary>
        /// Fetch a member via ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>MemberResponse</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<MemberResponse> GetMemberViaId(Guid id)
        {
            var member = await _commonRepo.GetAsync(m => m.User.Id == id, query => query
            .Include(u => u.User)
                .ThenInclude(ur => ur.UserRoles)
                    .ThenInclude(r => r.Role)
            .Include(p => p.Payments)
            .Include(a => a.Attendances)
            .Include(b => b.Bookings)
            .Include(m => m.Membership));

            if (member == null) { throw new ArgumentException("Invalid Member ID", nameof(id)); }

            return member.ToMemberResponse();
        }

        /// <summary>
        /// fetch all member
        /// </summary>
        /// <returns>Collection of Member Response</returns>
        public async Task<ICollection<MemberResponse>> GetAllMembersAsync()
        {
            var members = await _commonRepo.GetAllAsync(m =>
            !m.User.UserRoles.Any(r => r.Role.Name == Roles.Trainer.ToString() || r.Role.Name == Roles.Admin.ToString()),

            query => query
            .Include(u => u.User)
                .ThenInclude(ur => ur.UserRoles)
                    .ThenInclude(r => r.Role)
            .Include(p => p.Payments)
            .Include(a => a.Attendances)
            .Include(b => b.Bookings)
                .ThenInclude(c => c.Class)
                    .ThenInclude(t => t.Trainer)
                        .ThenInclude(u => u.User)
            .Include(m => m.Membership)
            );

            return members.Select(temp => temp.ToMemberResponse()).ToList();
        }

        /// <summary>
        /// Updates details of a Member
        /// </summary>
        /// <param name="request"></param>
        /// <returns>MemberResponse</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<MemberResponse> UpdateMemberDetailsAsync(MemberUpdateRequest request)
        {
            var user = await _commonRepo.GetAsync(m => m.UserId == request.Id, null);

            if (user == null) { throw new ArgumentNullException("Invalid User Id", nameof(request)); }

            if (request.DateOfBirth.HasValue) { user.DateOfBirth = request.DateOfBirth.Value; }
            if (request.Gender.HasValue) { user.Gender = request.Gender.Value; }

            await _commonRepo.UpdateAsync(user,
                m => m.DateOfBirth,
                m => m.Gender
                );

            return new MemberResponse
            {
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
            };
        }
    }
}
