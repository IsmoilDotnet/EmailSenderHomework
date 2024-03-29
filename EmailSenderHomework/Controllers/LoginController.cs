﻿using EmailSender.Application.Services;
using EmailSender.Infrastructure.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmailSenderHomework.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {

        private readonly IEmailService _emailService;

        public LoginController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserModel user)
        {
            bool isValidCredentials = await _emailService.VerifyCredentials(user);

            if (isValidCredentials)
            {
                return Ok("Login successful");
            }
            else
            {
                return BadRequest("Invalid credentials");
            }
        }
    }
}
