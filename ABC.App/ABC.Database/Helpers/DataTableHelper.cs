using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Database.Helpers
{
    public class CustomColumn
    {
        public string ColumnName { get; private set; }
        public Type ColumnType { get; private set; }

        public CustomColumn(string name, Type t)
        {
            ColumnName = name;
            ColumnType = t;
        }
    }

    public class DataTableHelper
    {
        public static DataTable CreateCustomTable(params CustomColumn[] columns)
        {
            var t = new DataTable();
            foreach (var col in columns)
            {
                t.Columns.Add(col.ColumnName, col.ColumnType);
            }
            return t;
        }

        public static void ReadFromDataReader(DbDataReader reader, ref DataTable output)
        {
            if (reader == null)
                return;

            int i;
            List<object> d = new List<object>();
            while (reader.Read())
            {
                d.Clear();
                for (i = 0; i < reader.FieldCount; i++)
                {
                    //if (reader.IsDBNull(i))
                    //    d.Add(null);
                    //else
                        d.Add(reader[i]);
                }
                output.Rows.Add(d.ToArray());
            }
        }
    }
}
