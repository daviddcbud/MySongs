using Microsoft.Data.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongTracker.Models
{
    public class SongLink
    {
        public int Id { get; set; }
        public int SongId { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public virtual Song Song { get; set; }
    }
    public class SongLinkConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SongLink>()
                       .ToTable("SongLinks")
                       .HasKey(s => new { s.Id });

            modelBuilder.Entity<SongLink>()
                        .Property(s => s.Id)
                        .ValueGeneratedOnAdd();

            modelBuilder.Entity<SongLink>()
                        .Property(s => s.Link)
                        .HasColumnType("varchar")
                        .HasMaxLength(1000)
                        .IsRequired();

            modelBuilder.Entity<SongLink>()
                       .Property(s => s.Description)
                       .HasColumnType("varchar")
                       .HasMaxLength(150)
                       .IsRequired();

            modelBuilder.Entity<SongLink>()
                        .Property(s => s.SongId)
                        .HasColumnType("int")
                        .IsRequired();


            modelBuilder.Entity<SongLink>().Ignore(x => x.IsDeleted);
            modelBuilder.Entity<SongLink>()
                        .HasOne(ctzc => ctzc.Song)
                        .WithMany(zc => zc.SongLinks)
                        .HasForeignKey(ctzc => new { ctzc.SongId });
        }
    }
}
