using Entities.Domain;
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

        public Task<ICollection<Membership>> GetAllMembershipsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
