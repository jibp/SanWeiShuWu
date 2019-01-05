using SanWeiShuWu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanWeiShuWu.Services
{
   public interface ISearchServices
    {
        Task<IEnumerable<Book>> SearchBK(string title);

        Task<IEnumerable<Book>> SearchDefaultBK();
    }
}
