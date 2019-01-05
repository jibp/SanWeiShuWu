using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanWeiShuWu.Models
{
    /// <summary>
    /// 书签
    /// </summary>
    public class Tags
    {
        public string Id { get; set; }
        public string TagName { get; set; }
        public int TagType { get; set; }
        public int ORDER { get; set; }
        public DateTime cjsj { get; set; }

        public int IsDelete { get; set; }

    }
}
