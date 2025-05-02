using Entities.Domain;
using Entities.DTO.PaymentDTO;
using Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly ICommonRepo<Payment> _commonRepo;

        public PaymentService(ICommonRepo<Payment> commonRepo)
        {
            _commonRepo = commonRepo;
        }

        public Task<PaymentResponse> CreatePaymentAsync(PaymentAddRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
