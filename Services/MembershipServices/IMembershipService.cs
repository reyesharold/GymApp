using Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MembershipServices
{
    public interface IMembershipService
    {
        Task<ICollection<Membership>> GetAllMembershipsAsync();
    }
}
