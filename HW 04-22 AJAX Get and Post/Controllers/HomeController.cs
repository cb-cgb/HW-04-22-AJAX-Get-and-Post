using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HW_04_22_AJAX_Get_and_Post.Models;

namespace HW_04_22_AJAX_Get_and_Post.Controllers
{
    public class HomeController : Controller
    {          

        public IActionResult Index()
        {
            return View();
        }
    }
}
