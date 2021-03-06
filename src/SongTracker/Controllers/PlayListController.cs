﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Mvc;
using SongTracker.Models;
using System.Data.SqlClient;
using SongTracker.Services;

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

        [Route("api/AddSongToPlayList")]
        [HttpPost]
        public async Task<IActionResult> AddSong([FromBody] SongPlayList model)
        {
            return await base.DoWorkAndReturnData(async () =>
            {
                model.Id = 0;
                var exists = await context.SongPlayLists.Where(x => x.PlayListId == model.PlayListId && x.SongId == model.SongId).FirstOrDefaultAsync();
                if (exists == null)
                {
                    context.SongPlayLists.Add(model);
                    await context.SaveChangesAsync();
                }
                return new ObjectResult(model.Id);
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
                var lookup = new Dictionary<int, SongVM>();
                foreach(var song in data.SongPlayLists)
                {
                    var songVM = new SongVM();
                    lookup.Add(song.SongId,songVM);
                    songVM.PlayListId = vm.Id;
                    songVM.Note = song.Note;
                    songVM.Artist = song.Song.Artist;
                    songVM.Key = song.Song.Key;
                    songVM.Name = song.Song.Name;
                    song.Order = song.Order;
                    songVM.Id =song.SongId;
                    vm.Songs.Add(songVM);
                    foreach (var link in song.Song.SongLinks)
                    {
                        songVM.Links.Add(link);
                        link.FormatLink();
                        

                    }
                }

                using (var conn = new SqlConnection(AzureAppSettings.ConnectionString))
                {
                    await conn.OpenAsync();
                    var cmd = new SqlCommand("", conn);
                    var startDate = DateTime.Now.AddYears(-1);
                    cmd.CommandText = string.Format("select sp.songid,count(*) as total from SongPlayLists sp inner join Playlists p on p.id=sp.PlayListId  " +
                "where date >= '{0}' group by sp.songid ", startDate);
                    var reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        var songid = int.Parse(reader["SongId"].ToString());
                        if (lookup.ContainsKey(songid))
                        {
                            var song = lookup[songid];
                            song.PlayCount = int.Parse(reader["total"].ToString());
                        }
                    }
                    reader.Close();
                }
                return new ObjectResult(vm);
            });
        }
    }
}
