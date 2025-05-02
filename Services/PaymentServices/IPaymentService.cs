using Entities.DTO.PaymentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PaymentServices
{
    public interface IPaymentService
    {
        Task<PaymentResponse> CreatePaymentAsync(PaymentAddRequest request);
    }
}
