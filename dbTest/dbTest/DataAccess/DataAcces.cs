using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace dbTest.DataAccess
{
    public class DataAcces
    {

        private string connectionString;
        public DataAcces()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["miconexion"].ConnectionString;
        }


        public DataTable QueryDataTable(string spName, Dictionary<string, object> parameters)
        {

            DataSet result = new DataSet();

            try
            {
                var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var conn = CreateConnection())
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = spName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 300;

                        foreach (var par in parameters)
                        {
                            cmd.Parameters.Add(new SqlParameter("@" + par.Key, par.Value ?? DBNull.Value));
                        }

                        var paramNmbError = new SqlParameter("@NmbError", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        cmd.Parameters.Add(paramNmbError);
                        var paramMsgError = new SqlParameter("@MsgError", SqlDbType.VarChar) { Direction = ParameterDirection.Output, Size = int.MaxValue };
                        cmd.Parameters.Add(paramMsgError);

                        var adapter = factory.CreateDataAdapter();
                        adapter.SelectCommand = (DbCommand)cmd;
                        adapter.Fill(result);

                        var code = paramNmbError.Value as int?;
                        var message = paramMsgError.Value as string;

                    }
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }

            return result.Tables[0];
        }

        public List<T> ToList<T>(string spName, Dictionary<string, object> parameters) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();
                var table = QueryDataTable(spName, parameters);
                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }


        private IDbConnection CreateConnection()
        {
            var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            var cnn = factory.CreateConnection();
            cnn.ConnectionString = this.connectionString;

            return cnn;
        }

    }
}