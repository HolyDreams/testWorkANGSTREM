using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using testWorkANGSTREM.Models;

namespace testWorkANGSTREM.Methods
{
    internal class ReadFile
    {
        string path = "dataBase.json";
        public DataBaseStruct GetDataBase()
        {
            if (!File.Exists(path))
                throw new Exception("Не найден файл dataBase.json, перенесите его в папку bin\\Debug\\net7.0-windows");

            var text = File.ReadAllText(path);
            return JsonSerializer.Deserialize<DataBaseStruct>(text);
        }

    }
}
