using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class SubscriberRepository : ISubscriberRepository
    {
        private readonly ApplicationDBContext _context;

        public SubscriberRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        
        public async Task<Subscriber?> CreateAsync(Subscriber subscriber)
        {
            var existingSubscriber = await _context.Subscribers.FirstOrDefaultAsync(s => s.Email == subscriber.Email);
            if (existingSubscriber != null)
            {
                return null;
            }
            await _context.Subscribers.AddAsync(subscriber);
            await _context.SaveChangesAsync();
            return subscriber;
        }

        public async Task<List<Subscriber>> GetAllAsync()
        {
            var subscribers = await _context.Subscribers.ToListAsync();
            return subscribers;
        }
    }
}