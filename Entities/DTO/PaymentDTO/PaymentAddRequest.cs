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
        [Range(0.001, int.MaxValue, ErrorMessage = "Enter Valid Amount")]
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "PaymentMethod can't be blank")]
        public PaymentMethod PaymentMethod { get; set; }

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
