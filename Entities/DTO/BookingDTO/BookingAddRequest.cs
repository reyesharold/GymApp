using Entities.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.BookingDTO
{
    public class BookingAddRequest
    {
        [Required(ErrorMessage = "Booking Date is required")]
        [DataType(DataType.DateTime)]
        public DateTime BookingDate { get; set; }
        [Required(ErrorMessage = "ClassId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Class ID")]
        public int ClassId { get; set; }
        [Required(ErrorMessage = "MemberId is required")]
        public Guid MemberId { get; set; }

        public Booking ToBooking()
        {
            return new Booking
            {
                BookingDate = BookingDate,
                ClassId = ClassId,
                UserId = MemberId
            };
        }
    }
}
