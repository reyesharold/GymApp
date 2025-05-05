using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.UserDTO
{
    public class RegisterMemberResponse
    {
        public bool Success { get; set; }
        public IEnumerable<IdentityError> Errors { get; set; }
        public Guid UserId { get; set; }
    }
}
