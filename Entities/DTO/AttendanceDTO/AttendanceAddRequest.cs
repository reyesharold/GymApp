using Entities.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.AttendanceDTO
{
    public class AttendanceAddRequest
    {
        [Required(ErrorMessage = "Check-In time can't be blank")]
        [DataType(DataType.DateTime)]
        public DateTime CheckInTime { get; set; }

        [Required(ErrorMessage = "Check-out time can't be blank")]
        [DataType(DataType.DateTime)]
        public DateTime CheckOutTime { get; set; }

        [Required(ErrorMessage = "User Id is Required")]
        public Guid? UserId {  get; set; }

        public Attendance ToAttendance()
        {
            return new Attendance
            {
                CheckInTime = CheckInTime,
                CheckOutTime = CheckOutTime,
                UserId = UserId
            };
        }
    }
}
