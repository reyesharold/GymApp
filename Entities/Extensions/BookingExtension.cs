using Entities.Domain;
using Entities.DTO.BookingDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Extensions
{
    public static class BookingExtension
    {
        public static BookingResponse ToBookingReponse(this Booking booking)
        {
            return new BookingResponse
            {
                Id = booking.Id,
                BookingDate = booking.BookingDate,
                ClassId = booking.ClassId,
                ClassName = booking.Class.ClassName,
                NewClassCapacity = booking.Class.Capacity,
                TrainerName = booking.Class.Trainer.User.DisplayName,
                MemberId = booking.UserId,
                MemberName = booking.Member.User.DisplayName,
            };
        }
    }
}
