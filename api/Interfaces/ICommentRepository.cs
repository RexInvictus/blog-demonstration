using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> CreateAsync(Comment comment);
        Task<Comment?> DeleteAsync(int id);
    }
}