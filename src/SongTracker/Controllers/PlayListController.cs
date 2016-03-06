using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Mvc;
using SongTracker.Models;

namespace SongTracker.Controllers
{
    public class PlayListController:BaseController
    {

        [Route("api/RemoveSong")]
        public async Task<IActionResult> RemoveSong([FromBody] SongVM vm)
        {
            return await base.DoWorkAndReturnData(async () =>
            {
                var song =await  context.SongPlayLists.Where(x => x.PlayListId == vm.PlayListId && x.SongId == vm.Id).SingleOrDefaultAsync();
                if(song!= null)
                {
                    context.SongPlayLists.Remove(song);
                }
                await context.SaveChangesAsync();
                return new ObjectResult(true);
            });
        }

        [Route("api/SavePlayList")]
        [HttpPost]
        public async Task<IActionResult> SavePlayList([FromBody] PlayListVM vm)
        {
            return await base.DoWorkAndReturnData(async () =>
            {
                PlayList playList;
                if(vm.Id > 0)
                {
                    playList = await context.PlayLists.Where(x => x.Id == vm.Id).SingleAsync();
                }
                else
                {
                    playList = new PlayList();
                    context.PlayLists.Add(playList);
                }
                playList.Date = vm.Date;
                playList.Description = vm.Description;
                await context.SaveChangesAsync();
                return new ObjectResult(playList.Id);
            });
        }
        [Route("api/PlayLists")]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            return await base.DoWorkAndReturnData(async () =>
            {
                var vm = await context.PlayLists.OrderByDescending(x => x.Date).ToListAsync();

                return new ObjectResult(vm);
            });
        }

        [Route("api/PlayList")]
        [HttpGet]
        public async Task<IActionResult> GetPlayList(int id)
        {
            return await base.DoWorkAndReturnData(async () =>
            {

                var data = await context.PlayLists.Include(x => x.SongPlayLists).ThenInclude(x => x.Song)
                .ThenInclude(x => x.SongLinks).Where(x=>x.Id==id).SingleAsync();
                var vm = new PlayListVM();
                vm.Date = data.Date;
                vm.Description = data.Description;
                vm.Id = data.Id;
                foreach(var song in data.SongPlayLists)
                {
                    var songVM = new SongVM();
                    songVM.PlayListId = vm.Id;
                    songVM.Note = song.Note;
                    songVM.Artist = song.Song.Artist;
                    songVM.Name = song.Song.Name;
                    song.Order = song.Order;
                    songVM.Id =song.SongId;
                    vm.Songs.Add(songVM);
                    foreach (var link in song.Song.SongLinks)
                    {
                        songVM.Links.Add(link);

                        

                    }
                }
                return new ObjectResult(vm);
            });
        }
    }
}
