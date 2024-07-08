using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IEmailRepository
    {
        Task SendToAllAsync(List<Subscriber> subscribers, string title, int id);
        Task SendEmailAsync(string to, string subject, string html);
    }
}