﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceA.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult index()
        {
            return View();
        }

  
    }


}
