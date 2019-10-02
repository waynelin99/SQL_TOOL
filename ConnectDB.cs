using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLTool
{
    public class ConnectDB<TEntity>
    where TEntity : class
    {
        public ConnectDB()
        {
        }
      
        public IQueryable<TResult> GetAlls<TResult>(string sql, string Host)
        where TResult : class
        {
            IQueryable<TResult> tResults;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectDB<TEntity>.GetConnString(Host)))
            {
                int? nullable = null;
                CommandType? nullable1 = null;
                tResults = SqlMapper.Query<TResult>(sqlConnection, sql, null, null, true, nullable, nullable1).AsQueryable<TResult>();
            }
            return tResults;
        }
        private static string GetConnString(string Host)
        {
            // Connection Timeout=5
            string tmp = $"Data Source={Host};Initial Catalog=abc;Persist Security Info=True;User ID=abc;Password=abc;";
            return tmp;
        }        
    }
}
