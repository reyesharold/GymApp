using Entities.DTO.UserDTO;
using Entities.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Extensions
{
    public static class UserExtension
    {
        public static UserResponse ToUserResponse(this UserApplication user)
        {
            return new UserResponse
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                userRoles = user.UserRoles.Select(temp => temp.ToApplicationUserRoleResponse()).ToList()
            };
        }
    }
}
