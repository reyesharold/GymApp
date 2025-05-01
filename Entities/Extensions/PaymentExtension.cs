using Entities.Domain;
using Entities.DTO.PaymentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Extensions
{
    public static class PaymentExtension
    {
        public static PaymentResponse ToPaymentResponse(this Payment payment)
        {
            return new PaymentResponse
            {
                PaymentId = payment.PaymentId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod,
                MemberId = payment.UserId,
                MemberName = payment.Member.User.DisplayName
            };
        }
    }
}
