using Entities.Identities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Enums;

namespace Entities.Seeders
{
    public static class RoleSeeder
    {
        public static async Task SeedRoleAsync(RoleManager<RoleApplication> roleManager)
        {
            foreach ( var role in Enum.GetNames(typeof(Enums.Roles)))
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new RoleApplication { Name = role});
                }
            }
        }
    }
}
