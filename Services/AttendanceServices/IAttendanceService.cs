using Entities.DTO.AttendanceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AttendanceServices
{
    public interface IAttendanceService
    {
        Task<AttendanceResponse> CreateAttendanceAsync(AttendanceAddRequest request);
        Task<AttendanceResponse> GetAttendanceAsync(int attendanceId);
        Task<AttendanceResponse> CheckOutAsync(int attendanceId);
        Task<ICollection<AttendanceResponse>> GetAttendanceOfMemberAsync(Guid memberId);
    }
}
