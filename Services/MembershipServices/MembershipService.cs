using Entities.Domain;
using Entities.DTO.MembershipDTO;
using Entities.Extensions;
using Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MembershipServices
{
    public class MembershipService : IMembershipService
    {
        private readonly ICommonRepo<Membership> _commonRepo;

        public MembershipService(ICommonRepo<Membership> commonRepo)
        {
            _commonRepo = commonRepo;
        }

        public async Task<ICollection<MembershipResponse>> GetAllMembershipsAsync()
        {
            var memberships = await _commonRepo.GetAllAsync();

            return memberships.Select(temp => temp.ToMembershipResponse()).ToList();
        }
    }
}
