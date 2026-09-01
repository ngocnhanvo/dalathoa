using System;
using System.Data;
using System.Data.SqlClient;

namespace Mbg.Data.SqlClient
{
    public static class SqlHelper
    {
        public static string connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["edoc2014ConnectionString"].ConnectionString;
        // Lay chuoi ket noi trong web.config
        // Tao va tra ve doi tuong SqlConnection
        public static SqlConnection GetConnection
        {
            get
            {
                String str = connectionString;
                SqlConnection cnn = new SqlConnection(str);
                return cnn;
            }
        }

        public static void close_connection()
        {
            SqlHelper.GetConnection.Close();
        }

        public static SqlCommand CreateCommand(String sql, params object[] nameValue)
        {
            SqlCommand Command = new SqlCommand(sql, SqlHelper.GetConnection);
            Command.CommandType = CommandType.Text;
            for (int i = 0; i < nameValue.Length; i += 2)
            {
                Command.Parameters.AddWithValue(nameValue[i].ToString(), nameValue[i + 1].ToString());
            }
            return Command;
        }

        public static SqlCommand CreateCommand(String sql, bool procdure, params object[] nameValue)
        {
            SqlCommand Command = new SqlCommand(sql, SqlHelper.GetConnection);
            Command.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < nameValue.Length; i += 2)
            {
                Command.Parameters.AddWithValue(nameValue[i].ToString(), nameValue[i + 1].ToString());
            }
            return Command;
        }

        public static DataTable Fill(DataTable dt, String sql, params object[] nameValue)
        {
            SqlCommand cmd = SqlHelper.CreateCommand(sql, nameValue);
            cmd.CommandTimeout = 60;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        public static DataTable Fill(DataTable dt, String sql, bool procdure, params object[] nameValue)
        {
            SqlCommand cmd = SqlHelper.CreateCommand(sql, true, nameValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        public static DataTable GetData(String sql, params object[] nameValue)
        {
            return Fill(new DataTable(), sql, nameValue);
        }

        public static DataTable GetDataFromProcedure(String sql, params object[] nameValue)
        {
            return Fill(new DataTable(), sql, true, nameValue);
        }

        public static int ExcuteNonQuery(String sql, params object[] nameValue)
        {
            SqlCommand cmd = SqlHelper.CreateCommand(sql, nameValue);
            cmd.CommandTimeout = 120;
            cmd.Connection.Open();
            int rows = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return rows;
        }

        public static string ExcuteNonQuery2(String sql, params object[] nameValue)
        {
            string a = "";
            SqlCommand cmd = SqlHelper.CreateCommand(sql, nameValue);
            cmd.CommandTimeout = 120;
            cmd.Connection.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                a = ex.Message;
            }
            cmd.Connection.Close();
            return a;
        }

        public static int ExcuteNonProcedure(String sql, params object[] nameValue)
        {
            SqlCommand cmd = SqlHelper.CreateCommand(sql, true, nameValue);
            cmd.CommandTimeout = 60;
            cmd.Connection.Open();
            int rows = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return rows;
        }

        public static SqlDataReader ExecuteReader(String sql, params object[] nameValue)
        {
            SqlCommand cmd = CreateCommand(sql, nameValue);
            cmd.CommandTimeout = 60;
            cmd.Connection.Open();
            SqlDataReader rd = cmd.ExecuteReader();

            return rd;
        }

        // Lấy một giá trị đơn dựa vào câu lệnh sql và danh sách tham số
        public static object ExecuteScalar(String sql, params object[] parameters)
        {
            SqlCommand cmd = CreateCommand(sql, parameters);
            cmd.CommandTimeout = 50000;
            cmd.Connection.Open();
            object value = cmd.ExecuteScalar();
            cmd.Connection.Close();
            return value;
        }

        // Lấy một giá trị đơn dựa vào câu lệnh sql và danh sách tham số
        public static object ExecuteScalarProcedure(String sql, params object[] parameters)
        {
            SqlCommand cmd = CreateCommand(sql, true, parameters);
            cmd.CommandTimeout = 60;
            cmd.Connection.Open();
            object value = cmd.ExecuteScalar();
            cmd.Connection.Close();
            return value;
        }

        public static string GetNewId()
        {
            string str = (string)SqlHelper.ExecuteScalar("select replace(newid(), '-', '')");
            return str;
        }
    }
}

