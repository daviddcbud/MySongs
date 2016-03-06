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
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public virtual Song Song { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set; }
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
                        .Property(s => s.CategoryId)
                        .HasColumnType("int")
                        .IsRequired();

            modelBuilder.Entity<SongTag>().Ignore(x => x.IsDeleted);
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
