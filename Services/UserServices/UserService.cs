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

        public UserService( UserManager<
            UserApplication> userManager, 
            IMemberService memberService, 
            SignInManager<UserApplication> signInManager)
        {
            _userManager = userManager;
            _memberService = memberService;
            _signInManager = signInManager;
        }


        /// <summary>
        /// adds a user to AspNetUsers and Members table, default role is set to Member
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Register Member Response</returns>
        public async Task<RegisterMemberResponse> RegisterMember(RegisterMemberAddRequest request)
        {
            var user = new UserApplication
            {
                DisplayName = request.RegisterDTO.Name,
                Email = request.RegisterDTO.Email,
                UserName = request.RegisterDTO.Email,
                Address = request.RegisterDTO.Address,
                PhoneNumber = request.RegisterDTO.PhoneNumber,
                AccountStatus = AccountStatus.Pending
            };

            IdentityResult createUserResult = await _userManager.CreateAsync(user, request.RegisterDTO.Password); //add user to AspNetUsers
            if (!createUserResult.Succeeded) 
            {

                return new RegisterMemberResponse
                {
                    Success = false,
                    Errors = createUserResult.Errors
                };
            }

            IdentityResult addRoleResult = await _userManager.AddToRoleAsync(user, nameof(Roles.Member)); // assign role
            if (!addRoleResult.Succeeded)
            {
                return new RegisterMemberResponse
                {
                    Success = false,
                    Errors = createUserResult.Errors
                };
            }

            request.MemberAddRequest.UserId = user.Id; // assigns userID to Member

            var member = await _memberService.AddMemberAsync(request.MemberAddRequest); //add user to Members

            await _signInManager.SignInAsync(user, isPersistent: false); 

            return new RegisterMemberResponse
            {
                Success = true,
                UserId = user.Id,
            };
        }

        /// <summary>
        /// Update Details of a user
        /// </summary>
        /// <param name="request"></param>
        /// <returns>User Response</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<UserResponse> UpdateUserDetails(MemberUpdateRequest request)
        {
            await _memberService.UpdateMemberDetailsAsync(request);
            
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) { throw new ArgumentNullException(nameof(request.Id)); }

            if (!string.IsNullOrEmpty(request.Name)) { user.DisplayName = request.Name; }
            if (!string.IsNullOrEmpty(request.Address)) { user.Address = request.Address; }

            IdentityResult result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) { throw new Exception("Unable to update Display Name"); }

            return new UserResponse
            {
                DisplayName = user.DisplayName,
            };
        }

        /// <summary>
        /// Deletes a user via Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Boolean</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> DeleteUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) { throw new ArgumentNullException("Invalid User Id",nameof(userId));}

            IdentityResult result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            else { return false; }
        }
    }
}
