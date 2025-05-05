using Entities.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Seeders
{
    public static class MembershipSeeder
    {
        public static void SeedMembership(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Membership>()
                .HasData(
                new Membership { MembershipId = 1, Name = "Monthly", Price = 850m, DurationInDays = 30 },
                new Membership { MembershipId = 2, Name = "Yearly", Price = 5600m, DurationInDays = 365 },
                new Membership { MembershipId = 3, Name = "Premium", Price = 11700m, DurationInDays = 700 }
                );
        }
    }
}
