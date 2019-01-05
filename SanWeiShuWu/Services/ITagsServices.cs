using SanWeiShuWu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanWeiShuWu.Services
{
   public interface ITagsServices
    {
        Task<IEnumerable<Tags>> ListTag();
    }
}
