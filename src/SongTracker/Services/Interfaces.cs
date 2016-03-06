using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongTracker.Services
{
    public interface IJsonFormattedErrorService
    {
        JsonResult CreateResult(HttpContext context, int statusCode, Controller controller, Exception ex);
    }
     
}
