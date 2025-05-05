using Entities.Domain;
using Entities.DTO.PaymentDTO;
using Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Repositories.Common;
using Services.MemberServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Enums;

namespace Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly ICommonRepo<Payment> _commonRepo;
        private readonly IMemberService _memberService;
        private readonly UserManager<UserApplication> _userManager;

        public PaymentService(ICommonRepo<Payment> commonRepo, IMemberService memberService,UserManager<UserApplication> userManager)
        {
            _commonRepo = commonRepo;
            _memberService = memberService;
            _userManager = userManager;
        }

        public async Task<PaymentResponse> CreatePaymentAsync(PaymentAddRequest request)
        {
            var member = await _memberService.GetMemberViaId(request.UserId); // get member via ID

            if(member.MembershipPrice != request.Amount) { throw new ArgumentException("Please pay exact amount!", nameof(request.Amount)); } //checks if the amount paid is equal to membership Prices

            var response = await _commonRepo.AddAync(request.ToPayment()); // add payment entity to DB

            var user = await _userManager.FindByIdAsync(request.UserId.ToString()); // gets the user from AspNetUsers
            if (user == null) { throw new ArgumentException("Invalid User Id", nameof(request.UserId)); }

            user.Member.User.AccountStatus = AccountStatus.Active; // updates the account status from pending to Active

            IdentityResult updateStatus = await _userManager.UpdateAsync(user); // updates the AspNetUsers DB
            if (!updateStatus.Succeeded) { throw new Exception("Unable to Update Account Status"); } // checking if update was successful

            return new PaymentResponse
            {
                PaymentId = response.PaymentId,
                Amount = response.Amount,
                PaymentDate = response.PaymentDate,
                PaymentMethod = response.PaymentMethod,
                MemberId = response.Member.UserId,
                MemberName = response.Member.User.DisplayName,

            };
        }
    }
}
