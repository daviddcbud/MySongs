using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using SongTracker.Models;

namespace SongTracker.Controllers
{
    public class SongsController:BaseController
    {

        [Route("api/Song")]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            return await base.DoWorkAndReturnData(async () =>
            {
                var data = await context.Songs.Include(x => x.SongLinks)
                .Include(x => x.SongTags).ThenInclude(x => x.Category).
                Include(x=>x.SongPlayLists).Where(x=>x.Id==id).
                SingleAsync();
                return new ObjectResult(data);
            });
        }

        [Route("api/Songs")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await base.DoWorkAndReturnData(async () =>
            {
                var data = await context.Songs.OrderBy(x => x.Name).ToListAsync();
                return new ObjectResult(data);
            });
        }

        [Route("api/SaveSong")]
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Song song)
        {
            return await base.DoWorkAndReturnData(async () =>
            {
                context.Attach<Song>(song);

                if (song.Id <= 0)
                {
                    song.Id = 0;
                    context.Entry<Song>(song).State = EntityState.Added;

                }
                else
                {
                    context.Entry<Song>(song).State = EntityState.Modified;
                }
                foreach(var link in song.SongLinks)
                {
                    if (link.Id <= 0 && link.IsDeleted) continue;

                    if (string.IsNullOrEmpty(link.Description)) link.Description = link.Link;
                    if(link.Id <=0 && !link.IsDeleted)
                    {
                        link.Id = 0;
                        context.Entry<SongLink>(link).State = EntityState.Added;
                        link.Song = song;
                        link.SongId = 0;

                    }
                    else if(link.IsDeleted){
                        context.Entry<SongLink>(link).State = EntityState.Deleted;
                    }
                    else
                    {
                        context.Entry<SongLink>(link).State = EntityState.Modified;
                    }
                }

                foreach (var link in song.SongTags)
                {
                    if (link.Id <= 0 && link.IsDeleted) continue;
                    if (link.Id <= 0)
                    {
                        link.Id = 0;
                        context.Entry<SongTag>(link).State = EntityState.Added;
                        link.Song = song;
                        link.SongId = 0;
                    }
                    else if (link.IsDeleted)
                    {
                        context.Entry<SongTag>(link).State = EntityState.Deleted;
                    }
                    else
                    {
                        context.Entry<SongTag>(link).State = EntityState.Modified;
                    }
                }
                await context.SaveChangesAsync();

                return new ObjectResult(song);
            });
        }
    }
}
