using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Database.Helpers
{
    //public class CustomParameter
    //{
    //    public string Name { get; private set; }
    //    public object Value { get; private set; }
    //    public DbType Type { get; private set; }

    //    public CustomParameter(string name, object value, DbType type = DbType.String)
    //    {
    //        Name = name;
    //        Value = value;
    //        Type = type;
    //    }
    //}

    public class DbMyCommand
    {
        public static DbCommand CreateCmd(DbContext context, string sql, CommandType type, params object[] values)//, params CustomParameter[] values)
        {
            DbCommand cmd = context.Database.Connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = type;
            var index = 0;
            foreach (var value in values)
            {
                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "@p" + index;
                parameter.Value = value;
                cmd.Parameters.Add(parameter);
                index++;
            }
            return cmd;
        }
    }
}
