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
    class BasketWork
    {
        public List<BasketStruct> GetBasket(Guid orderID)
        {
            try
            {
                var sqlQuery = $@"SELECT basket_id,
                                         bas.nomen_id,
                                         count,
                                         nom.name,
                                         nom.price,
                                         nom.bom_id,
                                         bom.name AS bomName
                                  FROM angstrem_basket AS bas INNER JOIN
                                         angstrem_nomen AS nom ON bas.nomen_id = nom.nomen_id AND
                                                                  bas.order_id = '{orderID}' AND
                                                                  bas.delete_state_code = 0 AND
                                                                  nom.delete_state_code = 0 INNER JOIN
                                         angstrem_bom AS bom ON nom.bom_id = bom.bom_id AND
                                                                nom.delete_state_code = 0";
                var res = SQLRequest.PostgreSQL(sqlQuery);
                if (res == null)
                    return new List<BasketStruct>();

                return (from DataRow a in res.Rows

                        select new BasketStruct
                        {
                            BasketID = (Guid)a["basket_id"],
                            ID = (int)a["nomen_id"],
                            Count = (int)a["count"],
                            Price = (double)a["price"],
                            Name = (string)a["name"],
                            BomCode = (short)a["bom_id"],
                            Bom = (string)a["bomName"],
                            SumPrice = (int)a["count"] * (double)a["price"]
                        }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<BasketStruct>();
            }
        }
        public void EditBasket(Guid id, int count)
        {
            try
            {
                var sqlQuery = $@"UPDATE angstrem_basket
                                  SET count = {count}
                                  WHERE basket_id = '{id}'";
                SQLRequest.PostgreSQL(sqlQuery);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void AddBasket(Guid orderID, int nomenID, int count)
        {
            try
            {
                var sqlQuery = $@"INSERT INTO angstrem_basket (nomen_id, order_id, count)
                                  VALUES ({nomenID},
                                          '{orderID}',
                                          {count})";
                SQLRequest.PostgreSQL(sqlQuery);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
