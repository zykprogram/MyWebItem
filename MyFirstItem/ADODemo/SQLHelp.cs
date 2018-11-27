using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODemo
{
    public class SQLHelp
    {
        private static string GetStringConn()
        {
            return ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
        }

        public static int ExecuteNoQuery(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(GetStringConn()))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //打开连接
                    conn.Open();
                    //传入执行SQL
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }//end cmd
            } //end conn
        }

        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(GetStringConn()))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //打开连接
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }//end cmd
            }//end conn
        }
    }
}
