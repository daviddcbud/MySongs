using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using SongTracker.Data;

namespace SongTracker.Controllers
{
    public class HomeController : BaseController
    {
       
         public IActionResult Index()
        {
            
            return View();
        }

       
         

        public IActionResult Error()
        {
            return View();
        }
    }
}
