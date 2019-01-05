using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanWeiShuWu.Common;
using SanWeiShuWu.Models;

namespace SanWeiShuWu.Services
{
    public class LiteratrueService : ILiteratureServices
    {
        private readonly ConnectionContext _context;
        public LiteratrueService(ConnectionContext context)
        {
            _context = context;
        }
        RedisHelperNewWrite redisWrite = new RedisHelperNewWrite(0);
        RedisHelperNewRead redisRead = new RedisHelperNewRead(0);
        public async Task<IEnumerable<Book>> ListBK()
        {
            IEnumerable<Book> res=new List<Book>();
            if (redisRead.IsSet("ListBK"))//redis中有这个KEY
            {
                res = redisRead.Get<List<Book>>("ListBK");
            }
            else //没有 就查数据库 并缓存到redis中去
            {
                res = await _context.Book.Where(n => n.isdelete == 0).OrderByDescending(n => n.CreatedAt).AsNoTracking().ToListAsync();
                if (res != null)
                {


                    #region 对数据处理
                    foreach (var item in res)
                    {
                        string coveridStr = item.CoverId;
                        string FileName = "";
                        if (item.TagType == 1)
                        {
                            FileName = "Net";
                        }
                        else if (item.TagType == 2)
                        {
                            FileName = "Python";
                        }
                        if (coveridStr.Contains("/"))
                        {
                            item.CoverId = coveridStr.Split('/')[1];

                            string pname = item.CoverId.ToString().Trim();
                            int index = pname.LastIndexOf(".");
                            if (index > 0 && index < pname.Length - 1)
                            {
                                // item.CoverId = pname.Substring(index + 1);
                                item.CoverId = FileName + "/" + item.CoverId;
                            }
                            else
                            {

                                item.CoverId = FileName + "/" + item.CoverId + ".jpg";
                            }
                        }
                        else
                        {
                            string pname = item.CoverId.ToString().Trim();
                            int index = pname.LastIndexOf(".");
                            if (index > 0 && index < pname.Length - 1)
                            {
                                // item.CoverId = pname.Substring(index + 1);
                                item.CoverId = FileName + "/" + item.CoverId;
                            }
                            else
                            {
                                item.CoverId = FileName + "/" + item.CoverId + ".jpg";
                            }
                        }

                    }
                    #endregion

                    redisWrite.Set("ListBK", res, 900);//缓存到redis中去
                }
            }
            return res;
        }

        public  Book Detail(string id) {
            Book book = new Book();
           var  res = _context.Book.Single(n => n.Id == id);
            if (res!=null)
            {
                string FileName = "";
                if (res.TagType == 1)
                {
                    FileName = "Net";
                }
                else if (res.TagType == 2)
                {
                    FileName = "Python";
                }

                string coveridStr = res.CoverId;
                if (coveridStr.Contains("/"))
                {
                    res.CoverId = coveridStr.Split('/')[1];
                }
                book.Id = res.Id;
                book.Title = res.Title;
                book.Author = res.Author;

                #region 处理图片地址
                if (coveridStr.Contains("/"))
                {
                    res.CoverId = coveridStr.Split('/')[1];

                    string pname = res.CoverId.ToString().Trim();
                    int index = pname.LastIndexOf(".");
                    if (index > 0 && index < pname.Length - 1)
                    {
                        // item.CoverId = pname.Substring(index + 1);
                        res.CoverId = FileName + "/" + res.CoverId;
                    }
                    else
                    {
                        res.CoverId = FileName + "/" + res.CoverId + ".jpg";
                    }
                }
                else
                {
                    string pname = res.CoverId.ToString().Trim();
                    int index = pname.LastIndexOf(".");
                    if (index > 0 && index < pname.Length - 1)
                    {
                        res.CoverId = FileName + "/" + res.CoverId;
                    }
                    else
                    {
                        res.CoverId = FileName + "/" + res.CoverId + ".jpg";
                    }
                }
                #endregion

                book.CoverId = res.CoverId;
                book.Intro = res.Intro;
                book.cbs = res.cbs;
                book.cbn = res.cbn;
                book.bben = res.bben;
                book.page = res.page;
                book.filename = res.filename;
                book.TagType = res.TagType;
              
               
            }

       
            return book;
        }
    }
}
