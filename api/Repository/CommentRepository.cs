using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Modes;
using ProfanityFilter;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;

        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public static bool ContainsLink(string str)
        {
            if (str.Contains("http") || str.Contains("www.") || str.Contains(".co"))
            {
                return true;
            }
            return false;
        }
        public async Task<Comment> CreateAsync(Comment comment)
        {
            // detect profanity or links
            var filter = new ProfanityFilter.ProfanityFilter();
            var strToTest = comment.Content + comment.Name;
            var swearList = filter.DetectAllProfanities(strToTest);
            if (swearList.Count > 0 || ContainsLink(strToTest)) throw new Exception();
            
            // if none create the comment
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if (comment == null)
            {
                return null;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        
    }
}