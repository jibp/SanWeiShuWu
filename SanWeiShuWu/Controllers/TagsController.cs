using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SanWeiShuWu.Common;
using SanWeiShuWu.Models;
using SanWeiShuWu.Services;

namespace SanWeiShuWu.Controllers
{
    public class TagsController : Controller
    {
        RedisHelperNewWrite redisWrite = new RedisHelperNewWrite(0);
        RedisHelperNewRead redisRead = new RedisHelperNewRead(0);
        private readonly ITagsServices _service;
        public TagsController(ITagsServices service)
        {
            _service = service;
        }
        // GET: Tags
        public async Task<ActionResult> Index()
        {
            IEnumerable<Tags> res = await _service.ListTag();

            return View(res);
        }
    }
}