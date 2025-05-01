using Entities.Domain;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.MemberDTO
{
    public class MemberAddRequest
    {
        [Required(ErrorMessage = "Date of Birth can't be blank")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth {  get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Membership is required")]
        public int MembershipId { get; set; }

        [Required(ErrorMessage = "User Id is required")]
        public Guid UserId { get; set; }
        public Member ToMember()
        {
            return new Member
            {
                DateOfBirth = DateOfBirth,
                Gender = Gender,
                MembershipId = MembershipId,
                UserId = UserId
            };
        }
    }
}
