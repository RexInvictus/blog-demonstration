using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Subscriber
{
    public class SubscriberDto
    {
        public int Id { get; set; }
        // subscriber name
        public string Name { get; set; } = string.Empty;
        // subscriber email
        public string Email { get; set; } = string.Empty;
    }
}