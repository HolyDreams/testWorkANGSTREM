using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testWorkANGSTREM.Models
{
    public class IdIntNameStruct
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class IdShortNameStruct : IdIntNameStruct
    {
        new public short ID { get; set; }
    }
    public class IdGuidNameStruct : IdIntNameStruct
    {
        new public Guid ID { get; set; }
    }
}
