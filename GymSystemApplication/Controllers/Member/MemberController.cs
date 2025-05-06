using Entities.ErrorViewModelClass;
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
    }
}
