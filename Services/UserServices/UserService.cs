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

namespace Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApplication> _userManager;
        private readonly IMemberService _memberService;
        private readonly SignInManager<UserApplication> _signInManager;

        public UserService( UserManager<UserApplication> userManager, IMemberService memberService, SignInManager<UserApplication> signInManager)
        {
            _userManager = userManager;
            _memberService = memberService;
            _signInManager = signInManager;
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

            IdentityResult result = await _userManager.CreateAsync(user, request.RegisterDTO.Password);
            if (!result.Succeeded)
            {
                throw new Exception("Error Creating User");
            }

            var member = await _memberService.AddMemberAsync(request.MemberAddRequest);

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
