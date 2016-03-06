using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongTracker.Models
{
    public class SearchOptions
    {

        public int SearchOption { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
    }
}
