using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcDiary.Models;

namespace MvcDiary.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Diary> Diaries { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Friend> Friends { get; set; }

        public DbSet<Root> Roots { get; set; }

        public DbSet<Word> Words { get; set; }

        public DbSet<Record> Records { get; set; }

        public DbSet<EssentialWord> Essentials { get; set; }
    }
}
