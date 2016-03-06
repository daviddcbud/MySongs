using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongTracker.Models
{
    public class PlayListVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public List<SongVM> Songs { get; set; }
        public PlayListVM()
        {
            Songs = new List<SongVM>();
        }

    }
    public class SongVM
    {
        public int Order { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public List<SongLink> Links { get; set; }
        public string Note { get; internal set; }
        public int PlayListId { get; set; }

        public SongVM()
        {
            Links = new List<SongLink>();
        }
    }
}
