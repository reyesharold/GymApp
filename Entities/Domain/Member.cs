using Entities.Enums;
using Entities.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Domain
{
    public class Member
    {
        public Guid UserId { get; set; }
        public UserApplication User {  get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public int MembershipId { get; set; }
        public Membership Membership { get; set; }
    }
}
