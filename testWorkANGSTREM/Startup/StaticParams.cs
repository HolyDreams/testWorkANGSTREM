using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testWorkANGSTREM.Methods;
using testWorkANGSTREM.Models;

namespace testWorkANGSTREM.Startup
{
    internal class StaticParams
    {
        static List<IdShortNameStruct> bomList;
        public static List<IdShortNameStruct> GetBom()
        {
            if (bomList == null || bomList.Count == 0)
            {
                var loader = new LoadBom();
                bomList = loader.GetBom();
            }

            return bomList;
        }
    }
}
