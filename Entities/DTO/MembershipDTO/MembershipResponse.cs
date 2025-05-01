using Entities.DTO.MemberDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.MembershipDTO
{
    public class MembershipResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public ICollection<MemberResponse> Members { get; set; }
    }
}
