using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using testWorkANGSTREM.Models;

namespace testWorkANGSTREM.Methods
{
    internal class LoadBom
    {
        public List<IdShortNameStruct> GetBom()
        {
            try
            {
                var sqlQuery = $@"SELECT bom_id,
                                         name
                                  FROM angstrem_bom
                                  WHERE delete_state_code = 0";
                var res = SQLRequest.PostgreSQL(sqlQuery);
                if (res == null || res.Rows.Count == 0)
                    return new List<IdShortNameStruct>();

                return (from DataRow a in res.Rows
                        select new IdShortNameStruct
                        {
                            ID = (short)a["bom_id"],
                            Name = (string)a["Name"]
                        }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<IdShortNameStruct>();
            }
        }
    }
}
