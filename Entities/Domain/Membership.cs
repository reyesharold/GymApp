﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Domain
{
    public class Membership
    {
        [Key]
        public int MembershipId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public ICollection<Member>? Members { get; set; } // one to many -> Member 
    }
}
