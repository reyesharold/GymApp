using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.MemberDTO
{
    public class MemberUpdateRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [DataType(DataType.Date)]
        public DateOnly? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public string? Address { get; set; }
    }
}
