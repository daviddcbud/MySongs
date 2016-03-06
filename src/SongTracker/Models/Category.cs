using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongTracker.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SongTag> SongTags { get; set; }
    }

    public class CategoryConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                       .ToTable("Categories")
                       .HasKey(s => new { s.Id });

            modelBuilder.Entity<Category>()
                        .Property(s => s.Id)
                        .ValueGeneratedOnAdd();

            modelBuilder.Entity<Category>()
                        .Property(s => s.Name)
                        .HasColumnType("varchar")
                        .HasMaxLength(150)
                        .IsRequired();

             



            modelBuilder.Entity<Category>()
                       .HasMany(x=>x.SongTags).WithOne(x=>x.Category)
                        .HasForeignKey(ctzc => new { ctzc.CategoryId });
        }
    }
}
