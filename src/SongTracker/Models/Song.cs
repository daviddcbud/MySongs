using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongTracker.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Key { get; set; }
        public ICollection<SongTag> SongTags { get; set; }
        public ICollection<SongLink> SongLinks { get; set; }
        public ICollection<SongPlayList> SongPlayLists { get; set; }
        
    }
    public class SongConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>()
                       .ToTable("Songs")
                       .HasKey(s => new { s.Id });

            modelBuilder.Entity<Song>()
                        .Property(s => s.Id)
                        .ValueGeneratedOnAdd();

            modelBuilder.Entity<Song>()
                        .Property(s => s.Name)
                        .HasColumnType("varchar")
                        .HasMaxLength(50)
                        .IsRequired();


            modelBuilder.Entity<Song>()
                        .Property(s => s.Artist)
                        .HasColumnType("varchar")
                        .HasMaxLength(50)
                        .IsRequired(false);

            modelBuilder.Entity<Song>()
                       .Property(s => s.Key)
                       .HasColumnType("varchar")
                       .HasMaxLength(50)
                       .IsRequired(false);

        }
    }
}
