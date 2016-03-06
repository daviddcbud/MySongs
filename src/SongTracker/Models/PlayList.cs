using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongTracker.Models
{
    public class PlayList
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public ICollection<SongPlayList> SongPlayLists { get; set; }
        public string FullTitle
        {
            get
            {
                var title = this.Date.ToShortDateString();

                if (!string.IsNullOrEmpty(Description)) title += "-" + this.Description;
                return title;
            }
        }
    }
    public class PlayListConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayList>()
                       .ToTable("PlayLists")
                       .HasKey(s => new { s.Id });

            modelBuilder.Entity<PlayList>()
                        .Property(s => s.Id)
                        .ValueGeneratedOnAdd();

            modelBuilder.Entity<PlayList>()
                        .Property(s => s.Description)
                        .HasColumnType("varchar")
                        .HasMaxLength(50)
                        .IsRequired(false);


            modelBuilder.Entity<PlayList>()
                        .Property(s => s.Date)
                        .HasColumnType("datetime")
                        .IsRequired();

            modelBuilder.Entity<PlayList>().Ignore(x => x.FullTitle);

        }
    }
}
