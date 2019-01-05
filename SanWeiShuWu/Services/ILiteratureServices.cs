using SanWeiShuWu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanWeiShuWu.Services
{
    public interface ILiteratureServices
    {
        Task<IEnumerable<Book>> ListBK();

        Book Detail(string id);
    }
}
