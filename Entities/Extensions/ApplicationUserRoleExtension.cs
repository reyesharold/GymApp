using Entities.DTO.ApplicationUserRoleDTO;
using Entities.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Extensions
{
    public static class ApplicationUserRoleExtension
    {
        public static ApplicationUserRoleResponse ToApplicationUserRoleResponse(this ApplicationUserRole userRole)
        {
            return new ApplicationUserRoleResponse
            {
                UserId = userRole.UserId,
                UserName = userRole.User.DisplayName,
                RoleId = userRole.RoleId,
                RoleName = userRole.Role.Name
            };
        }
    }
}
