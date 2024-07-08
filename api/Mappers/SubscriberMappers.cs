using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Subscriber;
using api.Models;

namespace api.Mappers
{
    public static class SubscriberMappers
    {
        public static SubscriberDto ToSubscriberDto(this Subscriber subscriber)
        {
            return new SubscriberDto
            {
                Id = subscriber.Id,
                Name = subscriber.Name,
                Email = subscriber.Email
            };
        }

        public static Subscriber FromCreateToSubscriber(this CreateSubscriberDto createSubscriberDto)
        {
            return new Subscriber
            {
                Name = createSubscriberDto.Name,
                Email = createSubscriberDto.Email.ToLower()
            };
        }
    }
}