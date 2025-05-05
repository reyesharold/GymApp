using Microsoft.AspNetCore.Mvc;
using Services.MemberServices;

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
        [HttpGet]
        public IActionResult DisplayMembers()
        {
            return View();
        }
    }
}
