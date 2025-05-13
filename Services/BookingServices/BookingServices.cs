using Entities.Domain;
using Entities.DTO.BookingDTO;
using Entities.Extensions;
using Microsoft.EntityFrameworkCore;
using Repositories.Common;
using Services.ClassServices;
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
        private readonly IClassService _classService;

        public BookingServices(ICommonRepo<Booking> commonRepo, IClassService classService)
        {
            _commonRepo = commonRepo;
            _classService = classService;

        }

        public async Task<BookingResponse> AddBooking(BookingAddRequest request)
        {
            var @class = await _classService.GetClassViaIdAsync(request.ClassId);
            if (@class == null) { throw new ArgumentException("Invalid Class ID", nameof(request.ClassId)); }
            if (@class.Capacity == 0) { throw new ArgumentException("Class is fully booked!", nameof(@class.Capacity)); }

            var booking = await _commonRepo.AddAync(request.ToBooking());
            await _classService.DecreaseClassCapacityViaBookingAsync(booking.ClassId);

            var response = await _commonRepo.GetAsync(b => b.Id == booking.Id, query => query
            .Include(c => c.Class)
                .ThenInclude(t => t.Trainer)
                    .ThenInclude(u => u.User)
            .Include(m => m.Member)
                .ThenInclude(u => u.User)
            );

            return response.ToBookingReponse();
        }
    }
}
