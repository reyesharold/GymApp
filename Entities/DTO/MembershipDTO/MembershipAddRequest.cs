using Entities.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.MembershipDTO
{
    public class MembershipAddRequest
    {
        [Required(ErrorMessage = "Membership Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.001, 10000, ErrorMessage = "Invalid Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Duration in days is required")]
        [Range(1,365, ErrorMessage = "Invalid Duration")]
        public int DurationInDays { get; set; }

        public Membership ToMembership()
        {
            return new Membership
            {
                Name = Name,
                Price = Price,
                DurationInDays = DurationInDays
            };
        }
    }
}
