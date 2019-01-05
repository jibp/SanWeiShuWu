using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SanWeiShuWu.Common;
using SanWeiShuWu.Models;

namespace SanWeiShuWu.Services
{
    public class TagsServices : ITagsServices
    {
        private readonly ConnectionContext _context;
        public TagsServices(ConnectionContext context)
        {
            _context = context;
        }
        RedisHelperNewWrite redisWrite = new RedisHelperNewWrite(0);
        RedisHelperNewRead redisRead = new RedisHelperNewRead(0);



        public async Task<IEnumerable<Tags>> ListTag()
        {
            IEnumerable<Tags> res = new List<Tags>();
            if (redisRead.IsSet("ListTags"))//redis中有这个KEY
            {
                res = redisRead.Get<List<Tags>>("ListTags");
            }
            else //没有 就查数据库 并缓存到redis中去
            {
               // string sql = " select b.TagName,b.TagType,(SELECT COUNT(*) FROM dbo.Book a WHERE a.TagType=b.TagType )TagTypeCount from Tags b where b.IsDelete=0 order by b.[ORDER] ";

                res = await _context.Tags.Where(n => n.IsDelete == 0).OrderBy(n => n.ORDER).AsNoTracking().ToListAsync();
                if (res!=null)
                {
                    redisWrite.Set("ListTags", res, 600);
                }
                
            }
            return res;

        }
    }
}
