using Microsoft.Data.Entity;
using SongTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongTracker.Data
{
    public class MyContext:DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);


        }

        public DbSet<Song> Songs { get; set; }
        public DbSet<SongTag> SongTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SongConfiguration.Configure(modelBuilder);
            SongTagConfiguration.Configure(modelBuilder);
        }

        }
}
