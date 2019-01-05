using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SanWeiShuWu.Common;

namespace SanWeiShuWu.Controllers
{
    public class MiddlePdfController : Controller
    {
        [HttpPost]
        public string Index(string file)
        {
            string jiamival = new DES_Helper().DecryptDES(file, "fuckyou!");
            return jiamival;
        }
    }
}