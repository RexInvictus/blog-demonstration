using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<GalleryItem> GalleryItems { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Trail> Trails { get; set; }
    }
}