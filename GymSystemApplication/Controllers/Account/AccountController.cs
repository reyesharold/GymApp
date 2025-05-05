using Entities.DTO.UserDTO;
using Entities.Enums;
using GymSystemApplication.Controllers.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public AccountController(
            IUserService userService, 
            IMembershipService membershipService)
        {
            _userService = userService;
            _membershipService = membershipService;
        }

        [Authorize(Policy = "NotAuthenticated")]
        [HttpGet]
        [Route("Account/Register")]
        public async Task<IActionResult> Register()
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



                //if (!string.IsNullOrEmpty(returnUrl) && !Url.IsLocalUrl(returnUrl))
                //{
                //    return LocalRedirect(returnUrl);
                //}

                return RedirectToAction(nameof(PaymentController.ProcessMembershipPayment), "Payment", new {id = response.User.Id});
            }
            catch
            {
                return View(request);
            }
        }

        [Authorize(Policy = "NotAuthenticated")]
        [HttpGet]
        [Route("Account/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Authorize(Policy = "NotAuthenticated")]
        [HttpPost]
        [Route("Account/Login")]
        public IActionResult Login(LoginDTO login)
        {
            return View();
        }


    }
}
