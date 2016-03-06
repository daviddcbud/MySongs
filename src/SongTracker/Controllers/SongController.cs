using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
namespace SongTracker.Controllers
{
    public class SongsController:BaseController
    {
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
    }
}
