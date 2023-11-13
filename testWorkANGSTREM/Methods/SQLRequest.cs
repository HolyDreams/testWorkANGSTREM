using Npgsql;
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
    internal class SQLRequest : IDisposable
    {
        static DataBaseStruct dbData;
        static NpgsqlConnection nc;

        internal static void ConnectToDB()
        {
            if (dbData == null)
            {
                loadData();
            }
            string connect = $@"Host='{dbData.Host}';
                                Username='{dbData.UserName}';
                                Password='{dbData.Password}';
                                Port={dbData.Port};
                                Database='{dbData.DataBase}'";
            nc = new NpgsqlConnection(connect);
            nc.Open();
        }
        internal static DataTable PostgreSQL(string sqlQuery)
        {
            if (nc == null || nc.State != ConnectionState.Open)
            {
                ConnectToDB();
            }

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sqlQuery, nc);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
        static void loadData()
        {
            var fileReader = new ReadFile();
            try
            {
                dbData = fileReader.GetDataBase();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        public void Dispose()
        {
            nc.Dispose();
        }
    }
}
