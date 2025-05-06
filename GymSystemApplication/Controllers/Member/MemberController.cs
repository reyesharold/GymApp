using Entities.ErrorViewModelClass;
using Microsoft.AspNetCore.Mvc;
using Services.MemberServices;
using System.Threading.Tasks;

namespace GymSystemApplication.Controllers.Member
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
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
    }
}
