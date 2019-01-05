using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanWeiShuWu.Common
{
    internal static class DalHelper
    {
        private static string _LiteratureConnectionString;
        static DalHelper()
        {
            _LiteratureConnectionString = "192.168.31.58;Initial Catalog=Literature;User ID=sa;Password=zhuzhuaini112618";
                //ConfigurationManager.ConnectionStrings["Literature"].ConnectionString;
        }


        public static string LiteratureConnectionString
        {
            get
            {
                return _LiteratureConnectionString;
            }
        }
    }
}
