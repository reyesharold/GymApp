using Entities.Domain;
using Entities.DTO.MemberDTO;
using Entities.DTO.UserDTO;
using Entities.Extensions;
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
using Services.PaymentServices;
using Entities.DTO.PaymentDTO;

namespace Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApplication> _userManager;
        private readonly IMemberService _memberService;
        private readonly SignInManager<UserApplication> _signInManager;
        private readonly IPaymentService _paymentService;

        public UserService( UserManager<
            UserApplication> userManager, 
            IMemberService memberService, 
            SignInManager<UserApplication> signInManager,
            IPaymentService paymentService)
        {
            _userManager = userManager;
            _memberService = memberService;
            _signInManager = signInManager;
            _paymentService = paymentService;
        }

        public async Task<MemberResponse> RegisterMember(RegisterMemberAddRequest request)
        {
            var user = new UserApplication
            {
                DisplayName = request.RegisterDTO.Name,
                Email = request.RegisterDTO.Email,
                UserName = request.RegisterDTO.Email,
                Address = request.RegisterDTO.Address,
                PhoneNumber = request.RegisterDTO.PhoneNumber,
            };

            IdentityResult createUserResult = await _userManager.CreateAsync(user, request.RegisterDTO.Password); //add user to AspNetUsers
            if (!createUserResult.Succeeded) { throw new Exception("Error Creating User");}

            IdentityResult addRoleResult = await _userManager.AddToRoleAsync(user, nameof(Roles.Member)); // assign role
            if (!addRoleResult.Succeeded) { throw new Exception("Error assigning role"); }

            request.MemberAddRequest.UserId = user.Id; // assigns userID to Member

            var member = await _memberService.AddMemberAsync(request.MemberAddRequest); //add user to Members

            request.PaymentAddRequest.UserId = user.Id;
            request.PaymentAddRequest.PaymentDate = DateTime.Now;
            if (request.PaymentAddRequest.Amount != member.Membership?.Price) { throw new Exception("Please Pay exact amount"); }

            await _paymentService.CreatePaymentAsync(request.PaymentAddRequest); // add payment

            await _signInManager.SignInAsync(user, isPersistent: false); 

            return new MemberResponse
            {
                User = user.ToUserResponse(),
                DateOfBirth =  member.DateOfBirth,
                Gender = member.Gender,
                Membership = member.Membership,
            };
        }
    }
}
