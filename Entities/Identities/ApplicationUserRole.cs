using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Identities
{
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public UserApplication User {  get; set; }
        public RoleApplication Role { get; set; }
    }
}
