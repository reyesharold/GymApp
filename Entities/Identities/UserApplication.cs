using Entities.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Identities
{
    public class UserApplication : IdentityUser<Guid>
    {
        public string DisplayName { get; set; }
        public Member Member { get; set; } // one to one -> Member
        public Trainer Trainer { get; set; } // one to one -> Trainer
    }
}
