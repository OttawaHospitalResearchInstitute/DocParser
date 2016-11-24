using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DocParser
{
    public class msSQLclass
    {
        public String conn = ConfigurationManager.ConnectionStrings["Contract_MSSQL"].ConnectionString;

        public DataTable GetData(String sql)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            reader.Close();
                            connection.Close();
                            return dt;
                        }
                    }
                }
            }
            catch (SqlException e) { throw e; }
            catch (Exception e) { throw e; }
        }

        public DataTable GetData(SqlCommand command)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    command.Connection = connection;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        reader.Close();
                        connection.Close();
                        return dt;
                    }
                }
            }
            catch (SqlException e) { throw e; }
            catch (Exception e) { throw e; }
        }

        public void ExecuteNonQuery(SqlCommand command)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    command.Connection = connection;
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
            }
            catch (SqlException e) { throw e; }
            catch (Exception e) { throw e; }
        }
    }
}