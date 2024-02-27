using EmailSender.Application.Services;
using EmailSender.Infrastructure.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmailSenderHomework.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CodeController : Controller
    {

        private readonly IEmailService _emailService;

        public CodeController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] EmailModel model)
        {
            await _emailService.SendEmailAsync(model);



            return Ok("Email Sent Successfully");
        }


        [HttpPost("check")]
        public async Task<IActionResult> CheckCode([FromBody] string code)
        {
            bool isCodeCorrect = await _emailService.CheckEmailAsync(code);

            if (isCodeCorrect)
            {
                return Ok("Code is correct");
            }
            else
            {
                return BadRequest("Incorrect code");
            }
        }


        [HttpPost("set-password")]
        public async Task<IActionResult> SetPassword([FromBody] UserModel user)
        {
            await _emailService.SetPassword(user);

            if (user.IsRegisteredOrNot)
            {
                return Ok("Already Registered");
            }

            return Ok("Password set successfully");
        }

    }
}
