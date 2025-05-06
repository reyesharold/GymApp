using Entities.DTO.UserDTO;
using Entities.Enums;
using Entities.ErrorViewModelClass;
using Entities.Identities;
using GymSystemApplication.Controllers.Member;
using GymSystemApplication.Controllers.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.MembershipServices;
using Services.UserServices;
using System.Threading.Tasks;

namespace GymSystemApplication.Controllers.Account
{
    public class AccountController : Controller
    {
        private  readonly IUserService _userService;
        private readonly IMembershipService _membershipService;
        private readonly SignInManager<UserApplication> _signInManager;

        public AccountController(
            IUserService userService, 
            IMembershipService membershipService,
            SignInManager<UserApplication> signInManager)
        {
            _userService = userService;
            _membershipService = membershipService;
            _signInManager = signInManager;
        }

        [Authorize(Policy = "NotAuthenticated")]
        [HttpGet]
        [Route("Account/Register")]
        public async Task<IActionResult> Register()
        {
            try
            {
                // dropdown for Gender
                ViewBag.Gender = Enum.GetValues(typeof(Gender))
                    .Cast<Gender>()
                    .Select(temp => new SelectListItem
                    {
                        Value = ((int)temp).ToString(),
                        Text = temp.ToString()
                    });

                //dropdown for MembershipType
                var memberships = await _membershipService.GetAllMembershipsAsync();

                ViewBag.MembershipType = memberships.Select(
                    temp => new SelectListItem
                    {
                        Value = temp.Id.ToString(),
                        Text = temp.Name
                    });

                return View();
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                };

                return View("Error", errorModel);
            }
        }
        [Authorize(Policy = "NotAuthenticated")]
        [HttpPost]
        [Route("Account/Register")]
        public async Task<IActionResult> Register(RegisterMemberAddRequest request)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    ViewBag.Errors = ModelState.Values.SelectMany(e => e.Errors).Select(d => d.ErrorMessage);
                    return View(request);
                }

                var response = await _userService.RegisterMember(request);
                if (!response.Success) 
                { 
                    foreach (var error in response.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(request); 
                }

                return RedirectToAction(nameof(PaymentController.ProcessMembershipPayment), "Payment", new {id = response.UserId});
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                };

                return View("Error", errorModel);
            }
        }

        [Authorize(Policy = "NotAuthenticated")]
        [HttpGet]
        [Route("Account/Login")]
        public IActionResult Login()
        {
            try
            { 
                return View();
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                };

                return View("Error", errorModel);
            }
        }

        [Authorize(Policy = "NotAuthenticated")]
        [HttpPost]
        [Route("Account/Login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            try
            { 
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage);
                    return View(login);
                }

                var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, isPersistent : false, lockoutOnFailure : false);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(MemberController.DisplayMembers), "Member");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username or Password");
                    return View(login);
                }
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                };

                return View("Error", errorModel);
            }
        }

        [Route("Account/Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}
