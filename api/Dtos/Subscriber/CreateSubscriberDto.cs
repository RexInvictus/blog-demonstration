using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Subscriber
{
    public class CreateSubscriberDto
    {
        [Required]
        // subscriber name
        public string Name { get; set; } = string.Empty;
        [Required]
        // subscriber email
        public string Email { get; set; } = string.Empty;
        [Required]
        // captcha token
        public string CaptchaToken { get; set; } = string.Empty;
    }
}
    
