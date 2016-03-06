using Microsoft.Data.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongTracker.Models
{
    public class SongPlayList
    {
        public int Id { get; set; }
        public int SongId { get; set; }
        public int PlayListId { get; set; }
        public string Note { get; set; }
        public int Order { get; set; }
        [JsonIgnore]
        public virtual Song Song { get; set; }
        public virtual PlayList PlayList { get; set; }

    }

    public class SongPlayListConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SongPlayList>()
                       .ToTable("SongPlayLists")
                       .HasKey(s => new { s.Id });

            modelBuilder.Entity<SongPlayList>()
                        .Property(s => s.Id)
                        .ValueGeneratedOnAdd();

            modelBuilder.Entity<SongPlayList>()
                        .Property(s => s.Note)
                        .HasColumnType("text")
                        .IsRequired(false);


            modelBuilder.Entity<SongPlayList>()
                        .Property(s => s.PlayListId)
                        .HasColumnType("int")
                        .IsRequired();

            modelBuilder.Entity<SongPlayList>()
                     .Property(s => s.Order)
                     .HasColumnType("int")
                     .IsRequired();


            modelBuilder.Entity<SongPlayList>()
                        .Property(s => s.SongId)
                        .HasColumnType("int")
                        .IsRequired();


            modelBuilder.Entity<SongPlayList>()
                        .HasOne(ctzc => ctzc.Song)
                        .WithMany(zc => zc.SongPlayLists)
                        .HasForeignKey(ctzc => new { ctzc.SongId });


            modelBuilder.Entity<SongPlayList>()
                        .HasOne(ctzc => ctzc.PlayList)
                        .WithMany(zc => zc.SongPlayLists)
                        .HasForeignKey(ctzc => new { ctzc.PlayListId });

        }
    }
}
