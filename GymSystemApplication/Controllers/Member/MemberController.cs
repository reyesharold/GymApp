using Entities.DTO.MemberDTO;
using Entities.Enums;
using Entities.ErrorViewModelClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.MemberServices;
using Services.UserServices;
using System.Threading.Tasks;

namespace GymSystemApplication.Controllers.Member
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IUserService _userService;

        public MemberController(IMemberService memberService,IUserService userService)
        {
            _memberService = memberService;
            _userService = userService;
        }

        [Route("Member/Display-all")]
        [Route("/")]
        [HttpGet]
        public async Task<IActionResult> DisplayMembers()
        {
            try
            {
                var members = await _memberService.GetAllMembersAsync();

                return View(members);
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

        /// <summary>
        /// Updates Member properties, uses viewModel
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [Route("Member/Update")]
        [HttpGet]
        public async Task<IActionResult> UpdateMember(Guid memberId)
        {
            try
            {
                var member = new MemberUpdateViewModel
                {
                    Member = await _memberService.GetMemberViaId(memberId)
                };

                ViewBag.Gender = Enum.GetValues(typeof(Gender))
                    .Cast<Gender>()
                    .Select(temp => new SelectListItem
                    {
                        Value = ((int)temp).ToString(),
                        Text = temp.ToString()
                    });

                return View(member);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel
                {
                    Message = ex.InnerException?.Message ?? ex.Message
                };
                return View("Error", errorModel);
            }
        }
        [Route("Member/Update")]
        [HttpPost]
        public async Task<IActionResult> UpdateMember(MemberUpdateViewModel request)
        {
            try
            {
                await _userService.UpdateUserDetails(request.UpdateRequest);

                return RedirectToAction(nameof(DisplayMembers));
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel
                {
                    Message = ex.InnerException?.Message ?? ex.Message
                };
                return View("Error", errorModel);
            }
        }

        [Route("Member/Confirm-Remove")]
        [HttpGet]
        public IActionResult ConfirmRemoveMember()
        {
            return View();
        }
    }
}
