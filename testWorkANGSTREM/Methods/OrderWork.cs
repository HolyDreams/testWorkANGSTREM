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
    class OrderWork
    {
        public List<IdGuidNameStruct> GetOrders()
        {
            try
            {
                var sqlQuery = $@"SELECT order_id,
                                         number,
                                         created_at
                                  FROM angstrem_orders
                                  WHERE delete_state_code = 0";
                var res = SQLRequest.PostgreSQL(sqlQuery);
                if (res == null)
                    return new List<IdGuidNameStruct>();

                return (from DataRow a in res.Rows
                        select new IdGuidNameStruct
                        {
                            ID = (Guid)a["order_id"],
                            Name = "Продажа №" + ((int)a["number"]).ToString("00000") + " от " + ((DateTime)a["created_at"]).ToString("dd.MM.yy")
                        }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<IdGuidNameStruct>();
            }
        }
        public Guid? CreateOrder()
        {
            try
            {
                var sqlQuery = $@"INSERT INTO angstrem_orders (number)
                                  SELECT CASE
                                            WHEN MAX(number) IS NULL THEN 0
                                            ELSE MAX(number)
                                         END + 1
                                  FROM angstrem_orders
                                  WHERE date_part('year', created_at) = {DateTime.Now.Year}
                                  RETURNING order_id";
                var res = SQLRequest.PostgreSQL(sqlQuery);
                return (Guid)res.Rows[0][0];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
