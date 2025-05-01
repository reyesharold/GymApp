using Entities.Domain;
using Entities.DTO.AttendanceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Extensions
{
    public static class AttendanceExtension
    {
        public static AttendanceResponse ToAttendaceResponse(this Attendance attendance)
        {
            return new AttendanceResponse
            {
                AttendanceId = attendance.AttendanceId,
                CheckInTime = attendance.CheckInTime,
                CheckOutTime = attendance.CheckOutTime,
                MemberId = attendance.UserId,
                MemberName = attendance.Member?.User.DisplayName
            };
        }
    }
}
