using Entities.DTO.UserDTO;
using Microsoft.AspNetCore.Mvc;
using Services.UserServices;
using System.Threading.Tasks;

namespace GymSystemApplication.Controllers.Account
{
    public class AccountController : Controller
    {
        private  readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("Account/Register")]
        public IActionResult Register()
        {
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
                return View("Error");
            }
        }

    }
}
