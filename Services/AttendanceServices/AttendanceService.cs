using Entities.Domain;
using Entities.DTO.AttendanceDTO;
using Entities.Extensions;
using Microsoft.EntityFrameworkCore;
using Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AttendanceServices
{
    public class AttendanceService : IAttendanceService
    {
        private readonly ICommonRepo<Attendance> _commonRepo;

        public AttendanceService(ICommonRepo<Attendance> commonRepo)
        {
            _commonRepo = commonRepo;
        }

        public async Task<AttendanceResponse> CreateAttendanceAsync(AttendanceAddRequest request)
        {
            request.CheckInTime = DateTime.Now;

            var attendance = await _commonRepo.AddAync(request.ToAttendance());

            var response = await _commonRepo.GetAsync(a => a.AttendanceId == attendance.AttendanceId, query => query
                .Include(m => m.Member)
                    .ThenInclude(u => u.User)
                );

            return new AttendanceResponse
            {
                AttendanceId = response.AttendanceId,
                CheckInTime = response.CheckInTime,
                MemberId = response.UserId,
                MemberName = response.Member!.User.DisplayName
            };
        }

        public async Task<AttendanceResponse> GetAttendanceAsync(int attendanceId)
        {
            var response = await _commonRepo.GetAsync(a => a.AttendanceId == attendanceId, query => query
                .Include(m => m.Member)
                    .ThenInclude(u => u.User)
                );

            return response.ToAttendaceResponse();
        }

        public async Task<AttendanceResponse> CheckOutAsync(int attendanceId)
        {
            var attendance = await _commonRepo.GetAsync(a => a.AttendanceId == attendanceId, query => query
                .Include(m => m.Member)
                    .ThenInclude(u => u.User)
                );
            attendance.CheckOutTime = DateTime.Now;

            await _commonRepo.UpdateAsync(attendance, 
                c => c.CheckOutTime
                );

            return attendance.ToAttendaceResponse();
        }
    }
}
