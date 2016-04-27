﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HelloEf
{
    public class HelloDbContext : DbContext
    {
        public HelloDbContext(DbContextOptions<HelloDbContext> options)
            : base(options)
        {
        }

        public DbSet<Greeting> Greetings { get; set; }
    }

    public class Greeting
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Phrase { get; set; }
        [Required]
        public string Language { get; set; }
    }
}