using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.BookingDTO
{
    public class BookingResponse
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public Guid MemberId { get; set; }
        public string MemberName { get; set; }
    }
}
