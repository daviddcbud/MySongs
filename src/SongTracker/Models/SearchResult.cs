using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongTracker.Models
{
    public class SearchResult
    {
        private Song song;
        public string Key
        {
            get
            {
                return song.Key;
            }
        }
        public string Categories
        {
            get
            {
                var list = "";
                foreach(var x in song.SongTags)
                {
                    if (!string.IsNullOrEmpty(list)) list += ", ";
                    list += x.Category.Name; 
                }
                return list;
            }
        }
        public string LastPlayed
        {
            get
            {
                var last = song.SongPlayLists.OrderByDescending(x => x.PlayList.Date).FirstOrDefault();
                if (last != null) return last.PlayList.Date.ToShortDateString();
                return null;

            }
        }
        public string Artist
        {
            get
            {
                return song.Artist;
            }
        }
        public string Title
        {
            get
            {
                return song.Name;
            }
        }
        public int Id
        {
            get
            {
                return song.Id; 
            }
               
        }

        public int PlayCount { get; internal set; }

        public SearchResult(Song song )
        {
            this.song = song;
        }
    }
}
