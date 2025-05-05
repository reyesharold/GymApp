using Entities.DTO.MemberDTO;
using Entities.DTO.PaymentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.UserDTO
{
    public class RegisterMemberAddRequest
    {
        public RegisterDTO RegisterDTO { get; set; }
        public MemberAddRequest MemberAddRequest { get; set; }
    }
}
