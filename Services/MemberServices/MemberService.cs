using Entities.Domain;
using Entities.DTO.MemberDTO;
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

        public Task<MemberResponse> AddMemberAsync(MemberAddRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
