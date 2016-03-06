using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using SongTracker.Models;
using System.Data.SqlClient;
using SongTracker.Services;

namespace SongTracker.Controllers
{
    public class SearchController : BaseController
    {

        [Route("api/Search")]
        [HttpPost]
        public async Task<IActionResult> Search([FromBody] SearchOptions vm)
        {
            return await base.DoWorkAndReturnData(async () =>
            {
            var list = new List<SearchResult>();
            var songs = new List<Song>();
                var lookup = new Dictionary<int, SearchResult>();
                if(vm.CategoryId > 0 && vm.SearchOption==0)
                {
                    songs = await SearchByCategory(vm.CategoryId);
                }
                else if (!string.IsNullOrEmpty(vm.Title) && vm.SearchOption == 1)
                {
                    songs = await SearchByTitle(vm.Title);
                }
                else
                {
                    songs = new List<Song>();
                }
                foreach(var s in songs)
                {
                    var result = new SearchResult(s);
                    list.Add(result);
                    lookup.Add(result.Id, result);
                }

                using(var conn=new SqlConnection(AzureAppSettings.ConnectionString))
                {
                    await conn.OpenAsync();
                    var cmd = new SqlCommand("", conn);
                    var startDate = DateTime.Now.AddYears(-1);
                    cmd.CommandText = string.Format("select sp.songid,count(*) as total from SongPlayLists sp inner join Playlists p on p.id=sp.PlayListId  " +
                "where date >= '{0}' group by sp.songid ", startDate);
                    var reader = await cmd.ExecuteReaderAsync();
                    while(reader.Read())
                    {
                        var id = int.Parse(reader["SongId"].ToString());
                        if (lookup.ContainsKey(id))
                        {
                            var song = lookup[id];
                            song.PlayCount = int.Parse(reader["total"].ToString());
                        }
                    }
                    reader.Close();
                }
                return new ObjectResult(list);

            });
        }

        private Task<List<Song>> ListAll()
        {
            return context.Songs.Include(x => x.SongLinks).Include(x => x.SongPlayLists).ThenInclude(x=>x.PlayList)
             .Include(x => x.SongTags).ThenInclude(x => x.Category).
             OrderBy(x => x.Name).ToListAsync();
        }

        private Task<List<Song>> SearchByTitle(string title)
        {
            return context.Songs.Include(x => x.SongLinks).Include(x => x.SongPlayLists).ThenInclude(x => x.PlayList)
              .Include(x => x.SongTags).ThenInclude(x => x.Category).
              Where(x => x.Name.Contains(title)).
              OrderBy(x => x.Name).ToListAsync();
        }

        private Task<List<Song>> SearchByCategory(int categoryId)
        {
            return context.Songs.Include(x => x.SongLinks).Include(x => x.SongPlayLists).ThenInclude(x => x.PlayList)
               .Include(x => x.SongTags).ThenInclude(x => x.Category).
               Where(x=>x.SongTags.Any(y=>y.CategoryId==categoryId)).
               OrderBy(x => x.Name).ToListAsync();
        }


        public static List<Category> Categories = new List<Category>();
        [Route("api/Tags")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await base.DoWorkAndReturnData(async () =>
            {
                if(Categories.Count > 0) return new ObjectResult(Categories);
                var data = await context.Categories.OrderBy(x => x.Name)
                .ToListAsync();
                data.Insert(0, new Category { Name = "", Id = 0 });
                Categories = data;
                return new ObjectResult(data);
            });
        }
    }
}
