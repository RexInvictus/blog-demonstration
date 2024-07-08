using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface IImageRepository
    {
        Task<string?> CreateAsync(IFormFile? file);
        Task<bool?> DeleteImageFromS3Async(string? url);
    }
}