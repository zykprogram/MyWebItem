using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
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

        /// <summary>
        /// 执行语句返回影响行数
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="parameters">语句中参数</param>
        /// <returns>数据库受影响行数</returns>
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

        /// <summary>
        /// 执行语句返回结果第一行第一列值
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="parameters">语句中参数</param>
        /// <returns>返回结果集第一行第一列值</returns>
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

        /// <summary>
        /// 执行语句返回DataReader型数据结果
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="parameters">语句中参数</param>
        /// <returns>返回DataReader数据集</returns>
        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(GetStringConn()))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //打开连接
                    conn.Open();
                    //传入执行语句    
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    //由于SqlDataReader使用时一直占用内存故在方法端无法释放Conn资源需在请求端使用完后释放
                    return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }//end conn
        }

        /// <summary>
        /// 执行语句返回DataTable数据表
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="parameters">语句中参数</param>
        /// <returns>返回DataTable数据表</returns>
        public static DataTable ExecuteDataTable(string sql, params SqlParameter[] parameters)
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql,GetStringConn()))
            {
                //创建数据保存表
                DataTable dt = new DataTable();
                adapter.SelectCommand.CommandText = sql;
                adapter.SelectCommand.Parameters.AddRange(parameters);
                //将查询的结果注入数据表中
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}
