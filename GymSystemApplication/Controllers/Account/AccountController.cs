using Entities.DTO.UserDTO;
using Entities.Enums;
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

            var memberships = await _membershipService.GetAllMembershipsAsync();

            //dropdown for MembershipType
            ViewBag.MembershipType = memberships.Select(
                temp => new SelectListItem
                {
                    Value = temp.MembershipId.ToString(),
                    Text = temp.Name
                });

            //dropdown for PaymentMethod
            ViewBag.PaymentMethod = Enum.GetValues(typeof(PaymentMethod))
                .Cast<PaymentMethod>()
                .Select(temp => new SelectListItem
                {
                    Value = ((int)temp).ToString(),
                    Text = temp.ToString(),
                });

            return View();
        }
        [HttpPost]
        [Route("Account/Register")]
        public async Task<IActionResult> Register(RegisterMemberAddRequest request, string returnUrl)
        {
            try
            {
                ModelState.Remove("returnUrl");

                if (!ModelState.IsValid)
                {
                    ViewBag.Errors = ModelState.Values.SelectMany(e => e.Errors).Select(d => d.ErrorMessage);
                    return View(request);
                }

                await _userService.RegisterMember(request);

                if (!string.IsNullOrEmpty(returnUrl) && !Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                return RedirectToAction("DisplayAllMembers", "Member");
            }
            catch
            {
                return View(request);
            }
        }

    }
}
