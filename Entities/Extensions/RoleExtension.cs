using Entities.DTO.RoleDTO;
using Entities.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Extensions
{
    public static class RoleExtension
    {
        public static RoleResponse ToRoleResponse(this RoleApplication role)
        {
            return new RoleResponse
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
        }
    }
}
