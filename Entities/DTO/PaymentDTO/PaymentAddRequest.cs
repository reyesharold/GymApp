using Entities.Domain;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.PaymentDTO
{
    public class PaymentAddRequest
    {
        [Required(ErrorMessage = "Amount can't be blank")]
        [Range(0.001, 100000, ErrorMessage = "Enter Valid Amount")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Payment Date can't be blank")]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "PaymentMethod can't be blank")]
        public PaymentMethod PaymentMethod { get; set; }

        [Required(ErrorMessage = "User Id can't be blank")]
        public Guid UserId { get; set; }

        public Payment ToPayment()
        {
            return new Payment
            {
                Amount = Amount,
                PaymentDate = PaymentDate,
                PaymentMethod = PaymentMethod,
                UserId = UserId
            };
        }
    }
}
