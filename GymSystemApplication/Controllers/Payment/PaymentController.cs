using Entities.DTO.PaymentDTO;
using Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.MemberServices;
using Services.PaymentServices;
using System.Threading.Tasks;

namespace GymSystemApplication.Controllers.Payment
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IMemberService _memberService;

        public PaymentController(IPaymentService paymentService,IMemberService memberService)
        {
            _paymentService = paymentService;
            _memberService = memberService;
        }

        [Route("Payment/Process")]
        [HttpGet]
        public async Task<IActionResult> ProcessMembershipPayment(Guid id)
        {
            var member = await _memberService.GetMemberViaId(id);

            ViewBag.Id = member.User.Id;
            ViewBag.Name = member.User.DisplayName;

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
        [Route("Payment/Process")]
        [HttpPost]
        public async Task<IActionResult> ProcessMembershipPayment(PaymentAddRequest payment)
        {
            await _paymentService.CreatePaymentAsync(payment);

            return RedirectToAction("DisplayMembers", "Member");
        }
    }
}