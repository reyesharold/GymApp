using Entities.DTO.BookingDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BookingServices
{
    public interface IBookingServices
    {
        Task<BookingResponse> AddBooking(BookingAddRequest request);
    }
}
