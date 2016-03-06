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
        public DbSet<Category> Categories { get; set; }
        public DbSet<PlayList> PlayLists { get; set; }
        public DbSet<SongPlayList> SongPlayLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SongConfiguration.Configure(modelBuilder);
            SongTagConfiguration.Configure(modelBuilder);
            SongLinkConfiguration.Configure(modelBuilder);
            CategoryConfiguration.Configure(modelBuilder);
            SongPlayListConfiguration.Configure(modelBuilder);
            PlayListConfiguration.Configure(modelBuilder);
        }

        }
}
