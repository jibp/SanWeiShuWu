using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SanWeiShuWu.Common;
using SanWeiShuWu.Models;
using SanWeiShuWu.Services;

namespace SanWeiShuWu.Controllers
{
    public class LiteratureController : Controller
    {

        RedisHelperNewWrite redisWrite = new RedisHelperNewWrite(0);
        RedisHelperNewRead redisRead = new RedisHelperNewRead(0);
        private readonly ILiteratureServices _service;
        public LiteratureController(ILiteratureServices service)
        {
            _service = service;
        }

        // 默认加载
        public async Task<ActionResult> Index(int tag = 1)
        {
           
            IEnumerable<Book> res = await _service.ListBK();

            
            var model = from n in res where Convert.ToInt32(n.TagType) == tag select n;
            //var model = res.ToPagedList(pageIndex, pageSize);

            ViewBag.TagVal = tag;
            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView("_commentpost", model);
            //}
            //else
            //{
          
            return View("Index", model);
            //}
        }

        public  ActionResult Detail(string id)
        {
            Book book =  _service.Detail(id);

            string FileName = "";
            if (book.TagType == 1)
            {
                FileName = "Net";
            }
            else if (book.TagType == 2)
            {
                FileName = "Python";
            }

            var url = "/File/" + FileName + "/" + book.filename;
            var jiami = new DES_Helper().EncryptDES(url, "fuckyou!");//加密
            ViewBag.FileAddress = jiami;

            return View("Detail", book);
        }


    }
}