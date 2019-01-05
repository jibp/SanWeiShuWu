using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SanWeiShuWu.Common;
using SanWeiShuWu.Models;

namespace SanWeiShuWu.Services
{
    public class SearchService : ISearchServices
    {
        private readonly ConnectionContext _context;
        public SearchService(ConnectionContext context)
        {
            _context = context;
        }
        RedisHelperNewWrite redisWrite = new RedisHelperNewWrite(0);
        RedisHelperNewRead redisRead = new RedisHelperNewRead(0);
        public async Task<IEnumerable<Book>> SearchBK(string title)
        {
            IEnumerable<Book> res = new List<Book>();
            if (redisRead.IsSet("ListBK"))//redis中有这个KEY
            {
                res = redisRead.Get<List<Book>>("ListBK");
                res = (from n in res where n.Title.Contains($"{title}") orderby n.CreatedAt select n).ToList();
            }
            else //没有 就查数据库 并缓存到redis中去
            {
                res = await _context.Book.Where(n => n.isdelete == 0 && n.Title.Contains($"{title}")).OrderByDescending(n => n.CreatedAt).AsNoTracking().ToListAsync();
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

        public async Task<IEnumerable<Book>> SearchDefaultBK()
        {
            IEnumerable<Book> res = new List<Book>();
            if (redisRead.IsSet("ListBK"))//redis中有这个KEY
            {
                res = redisRead.Get<List<Book>>("ListBK");
            }
            else //没有 就查数据库 并缓存到redis中去
            {
                res = await _context.Book.Where(n => n.isdelete == 0 ).OrderByDescending(n => n.CreatedAt).AsNoTracking().ToListAsync();
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
    }
}
