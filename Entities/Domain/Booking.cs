using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Domain
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public int ClassId { get; set; } // foreignKey for Class
        public Class Class { get; set; } // one to one -> Class
        public Guid UserId { get; set; } // foreignKey for Member
        public Member Member { get; set; } // one to one -> Member
    }
}
