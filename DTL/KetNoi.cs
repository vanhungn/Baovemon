using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL
{
    public class KetNoi
    {
        public static string connStr = @"Data Source = LAPTOP-NG3J2HSN\KTEAM; Initial Catalog = quanlykho; Integrated Security=True";
          protected SqlConnection conn = new SqlConnection("Data Source = LAPTOP-NG3J2HSN\\KTEAM; Initial Catalog = quanlykho; Integrated Security=True");
        public static SqlConnection GetConn()
        {
            return new SqlConnection(connStr);
        }
    }
}
