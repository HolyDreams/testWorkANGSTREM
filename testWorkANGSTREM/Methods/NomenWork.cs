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
    internal class NomenWork
    {
        public List<NomenStruct> GetNomen()
        {
            try
            {
                var sqlQuery = $@"SELECT nomen_id,
                                         nom.name,
                                         nom.bom_id,
                                         bom.name AS bomName,
                                         price
                                  FROM angstrem_nomen AS nom INNER JOIN
                                         angstrem_bom AS bom ON nom.bom_id = bom.bom_id AND
                                                                nom.delete_state_code = 0 AND
                                                                bom.delete_state_code = 0";
                var res = SQLRequest.PostgreSQL(sqlQuery);
                if (res == null || res.Rows.Count == 0)
                    return new List<NomenStruct>();

                return (from DataRow a in res.Rows
                        select new NomenStruct
                        {
                            ID = (int)a["nomen_id"],
                            Name = (string)a["name"],
                            Price = (double)a["price"],
                            Bom = (string)a["bomName"],
                            BomCode = (short)a["bom_id"]
                        }).OrderBy(a => a.Name).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<NomenStruct>();
            }
        }
        public void AddNomen(NomenStruct nomen)
        {
            try
            {
                var sqlQuery = $@"INSERT INTO angstrem_nomen (name, bom_id, price)
                                  VALUES ('{nomen.Name}',
                                          {nomen.BomCode},
                                          '{nomen.Price}')";
                SQLRequest.PostgreSQL(sqlQuery);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void DelNomen(int id)
        {
            try
            {
                var sqlQuery = $@"UPDATE angstrem_nomen
                                  SET delete_state_code = 1
                                  WHERE nomen_id = {id}";
                SQLRequest.PostgreSQL(sqlQuery);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool CheckNomen(int id)
        {
            try
            {
                var sqlQuery = $@"SELECT *
                                  FROM angstrem_nomen
                                  WHERE nomen_id = {id}";
                var res = SQLRequest.PostgreSQL(sqlQuery);
                return res != null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
