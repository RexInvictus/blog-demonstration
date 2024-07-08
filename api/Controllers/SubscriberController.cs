using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading.Tasks;
using api.Dtos.Subscriber;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace api.Controllers
{
    [Route("api/subscriber")]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly ISubscriberRepository _subscriberRepo;
        private readonly IConfiguration _config;


        public SubscriberController(ISubscriberRepository subscriberRepo, IConfiguration config)
        {
            _subscriberRepo = subscriberRepo;
            _config = config;
        }

        private static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith('.'))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        private static async Task<bool> IsValidCaptcha(string captcha)
        {
            var apiUrl = $"https://www.google.com/recaptcha/api/siteverify?secret=6LeiyKYpAAAAAAldXaAaOQGEYrgdQdq-viq23HEX&response={captcha}";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var captchaResponse = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

                    if (captchaResponse != null && captchaResponse?.success != null)
                    {
                        return captchaResponse?.success;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var subscribers = await _subscriberRepo.GetAllAsync();
            return Ok(subscribers);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSubscriberDto createSubscriberDto)
        {
            // validate captcha
            var isValidCaptcha = await IsValidCaptcha(createSubscriberDto.CaptchaToken);
            if (!isValidCaptcha) return StatusCode(500);

            var adminName = _config["AdminCredentials:Name"];
            var adminEmail = _config["AdminCredentials:Email"];

            if (createSubscriberDto.Name == adminName && createSubscriberDto.Email == adminEmail)
            {
                var accessToken = _config["AdminCredentials:Token"];
                return Ok(new { accessToken });
            }
            else
            {
                if (IsValidEmail(createSubscriberDto.Email))
                {
                    var subscriber = await _subscriberRepo.CreateAsync(createSubscriberDto.FromCreateToSubscriber());
                    return Ok(subscriber);
                }
                else
                {
                    return StatusCode(500);
                }
            }
        }
    }
}