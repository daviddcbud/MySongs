using Microsoft.AspNet.Mvc;
using SongTracker.Data;
using SongTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SongTracker.Controllers
{
    public class BaseController:Controller
    {
        [FromServices]
        public  MyContext context { get; set; }
        [FromServices]
        public IJsonFormattedErrorService JsonFormattedErrorService { get; set; }
         
        public async Task<IActionResult> DoWorkAndReturnData(Func<Task<ObjectResult>> action)
        {
            try
            {
#if(DEBUG)
                var now = DateTime.Now;
                var result = await action();

                var after = DateTime.Now;
                var timeItTook = after.Subtract(now).Milliseconds;
                System.Diagnostics.Debug.WriteLine("#TIME | " + this.GetType().ToString().PadRight(25, " "[0]) + " | " + timeItTook);
                return result;

#else
                return await action();
#endif

            }
            catch (Exception ex)
            {
                

                return JsonFormattedErrorService.CreateResult(HttpContext, (int)HttpStatusCode.BadRequest, this, ex);
            }
        }

        public async Task<IActionResult> DoVoidWork(Func<Task> action)
        {
            try
            {
                await action();
                return new HttpOkResult();
            }
            catch (Exception ex)
            {
              
                return JsonFormattedErrorService.CreateResult(HttpContext, (int)HttpStatusCode.BadRequest, this, ex);
            }
        }

    }
}
