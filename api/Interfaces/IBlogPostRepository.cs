using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.BlogPost;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IBlogPostRepository
    {
        Task<BlogPost?> GetByIdAsync(int id);
        Task<List<BlogPost>> GetAllAsync(QueryObject query);
        Task<BlogPost> CreateAsync(BlogPost blogPost);
        Task<BlogPost?> EditAsync(Trail? trial, EditBlogPostDto editBlogPostDto);
        Task<BlogPost?> DeleteAsync(int id);
        Task<BlogPost?> UpdateViewCountAsync(int id);
    }
}