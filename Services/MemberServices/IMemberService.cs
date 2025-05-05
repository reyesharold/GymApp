using Entities.DTO.MemberDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MemberServices
{
    public interface IMemberService
    {
        Task<MemberResponse> AddMemberAsync(MemberAddRequest request);
        Task<MemberResponse> GetMemberViaId(Guid id);
    }
}
