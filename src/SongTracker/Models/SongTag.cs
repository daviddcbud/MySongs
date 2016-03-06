using Microsoft.Data.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongTracker.Models
{
    public class SongTag
    {
        public int Id { get; set; }
        public int SongId { get; set; }
        public string Tag { get; set; }
        [JsonIgnore]
        public virtual Song Song { get; set; }
    }

    public class SongTagConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SongTag>()
                       .ToTable("SongTags")
                       .HasKey(s => new { s.Id });

            modelBuilder.Entity<SongTag>()
                        .Property(s => s.Id)
                        .ValueGeneratedOnAdd();
             
            modelBuilder.Entity<SongTag>()
                        .Property(s => s.Tag)
                        .HasColumnType("varchar")
                        .HasMaxLength(50)
                        .IsRequired();


            modelBuilder.Entity<SongTag>()
                        .Property(s => s.SongId)
                        .HasColumnType("int")
                        .IsRequired();



            modelBuilder.Entity<SongTag>()
                        .HasOne(ctzc => ctzc.Song)
                        .WithMany(zc => zc.SongTags)
                        .HasForeignKey(ctzc => new { ctzc.SongId  });
        }
    }
}
