using Entities.Domain;
using Entities.DTO.BookingDTO;
using Entities.Extensions;
using Microsoft.EntityFrameworkCore;
using Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BookingServices
{
    public class BookingServices : IBookingServices
    {
        private readonly ICommonRepo<Booking> _commonRepo;

        public BookingServices(ICommonRepo<Booking> commonRepo)
        {
            _commonRepo = commonRepo;
        }

        public async Task<BookingResponse> AddBooking(BookingAddRequest request)
        {
            var booking = await _commonRepo.AddAync(request.ToBooking());

            var response = await _commonRepo.GetAsync(b => b.Id == booking.Id, query => query
            .Include(c => c.Class)
                .ThenInclude(t => t.Trainer)
                    .ThenInclude(u => u.User)
            .Include(m => m.Member)
                .ThenInclude(u => u.User)
            );

            //add logic to subract CLass capacity

            return response.ToBookingReponse();
        }
    }
}
