using Entities.Domain;
using Entities.DTO.MembershipDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Extensions
{
    public static class MembershipExtension
    {
        public static MembershipResponse ToMembershipResponse(this Membership membership)
        {
            return new MembershipResponse
            {
                Id = membership.MembershipId,
                Name = membership.Name,
                Price = membership.Price,
                DurationInDays = membership.DurationInDays,
                Members = membership.Members.Select(m => m.ToMemberResponse()).ToList()
            };
        }
    }
}
