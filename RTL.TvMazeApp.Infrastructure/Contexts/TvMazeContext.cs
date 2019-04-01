using Microsoft.EntityFrameworkCore;
using RTL.TvMazeApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RTL.TvMazeApp.Infrastructure.Contexts
{
    public class TvMazeContext : DbContext
    {
        public TvMazeContext(DbContextOptions<TvMazeContext> options) : base(options) { }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (modelBuilder != null)
            {
                modelBuilder.Entity<Show>();
                modelBuilder.Entity<Person>();
            }
        }
    }
}
