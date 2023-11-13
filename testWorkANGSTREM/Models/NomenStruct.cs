using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testWorkANGSTREM.Models
{
    internal class NomenStruct : IdIntNameStruct
    {
        public double Price { get; set; }
        public short BomCode { get; set; }
        public string Bom { get; set; }
    }
    internal class BasketStruct : NomenStruct
    {
        public int Count { get; set; }
        public Guid BasketID { get; set; }
        public double SumPrice { get; set; }
    }
}