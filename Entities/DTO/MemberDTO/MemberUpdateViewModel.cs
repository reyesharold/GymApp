using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.MemberDTO
{
    public class MemberUpdateViewModel
    {
        public MemberResponse Member {  get; set; }
        public MemberUpdateRequest? UpdateRequest { get; set; }
    }
}
