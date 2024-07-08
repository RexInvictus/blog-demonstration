using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ICommentRepository _commentRepo;
        private readonly IEmailRepository _emailRepo;

        public CommentController(ApplicationDBContext context, ICommentRepository commentRepo, IEmailRepository emailRepo)
        {
            _context = context;
            _commentRepo = commentRepo;
            _emailRepo = emailRepo;
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

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentDto createCommentDto)
        {
            try
            {
                // validate captcha
                var isValidCaptcha = await IsValidCaptcha(createCommentDto.CaptchaToken);
                if (!isValidCaptcha) return StatusCode(500);

                var comment = createCommentDto.FromCreateToComment();
                await _commentRepo.CreateAsync(comment);
                try
                {
                    await _emailRepo.SendEmailAsync("lietuvisanglijoje@gmail.com", "New Comment", $"<html><body><p>{comment.Name} posted a comment on one of your posts. It says:</p><p>{comment.Content}</p><a href='www.modestastravels.com/blog/{comment.BlogPostId}'>Click to view</a></body></html>");
                }
                catch
                {
                    // do nothing
                }
                return Ok(comment);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [TokenAuth]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var comment = await _commentRepo.DeleteAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}