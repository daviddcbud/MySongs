using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using SongTracker.Models;

namespace SongTracker.Controllers
{
    public class CategoryController:BaseController
    {
        [Route("api/Categories")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await base.DoWorkAndReturnData(async () =>
            {
                 
                var data = await context.Categories.OrderBy(x => x.Name)
                .ToListAsync();
                
                return new ObjectResult(data);
            });
        }

        [Route("api/Category")]
        [HttpPost]
        public async Task<IActionResult> Save([FromBody]Category category)
        {
            return await base.DoWorkAndReturnData(async () =>
            {
                Category cat = null;
                if (category.Id > 0)
                {
                  cat = await context.Categories.Where(x => x.Id == category.Id).SingleOrDefaultAsync();
                }
                else
                {
                    cat = new Category();
                    context.Categories.Add(cat);
                }
                cat.Name = category.Name;
                await context.SaveChangesAsync();
                SearchController.Categories.Clear();

                return new ObjectResult(cat);
            });
        }

        [Route("api/CategoryDelete")]
        [HttpPost]
        public async Task<IActionResult> CategoryDelete([FromBody]Category category)
        {
            return await base.DoWorkAndReturnData(async () =>
            {
                Category cat = null;
                if (category.Id > 0)
                {
                    cat = await context.Categories.Where(x => x.Id == category.Id).SingleOrDefaultAsync();
                    context.Categories.Remove(cat);
                    await context.SaveChangesAsync();
                }
               
               
                SearchController.Categories.Clear();

                return new ObjectResult(cat);
            });
        }


    }
}
