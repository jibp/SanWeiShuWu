
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SanWeiShuWu.Common;
using SanWeiShuWu.Models;
using SanWeiShuWu.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace SanWeiShuWu.Controllers
{
    public class SearchController : Controller
    {
      
        private readonly ISearchServices _service;
        public SearchController(ISearchServices service)
        {
            _service = service;
        }

        // GET: Search
        public async Task<ActionResult> Index()
        {

            IEnumerable<Book> res = await _service.SearchDefaultBK();

            return View("Index", res);


        }


        [HttpPost]
        public async Task<ActionResult> Index(string title = "")
        {

            IEnumerable<Book> res = await _service.SearchBK(title);
          

            return PartialView("_commentpost", res);
        }


    }
}