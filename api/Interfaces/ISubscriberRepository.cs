using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface ISubscriberRepository
    {
        Task<Subscriber?> CreateAsync(Subscriber subscriber);
        Task<List<Subscriber>> GetAllAsync();
    }
}