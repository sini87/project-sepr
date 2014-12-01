using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CDDSS_API.Models
{
    public class DBConnection
    {
        private static DBConnection instance;
        private string connStr = "Data Source=54.93.154.67;Initial Catalog=cddss;Persist Security Info=True;User ID=cddss;Password=passme";
        private SqlConnection conn;

        private DBConnection()
        {
            conn = new SqlConnection(connStr);
        }

        public SqlConnection Connection
        {
            get
            {
                return conn;
            }
        }

        public static DBConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    return new DBConnection();
                }
                return instance;
            }
        }
    }
}