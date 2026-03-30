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
        public static string connStr = @"Data Source=DESKTOP-3INS5UR\MSSQLSERVER01;Initial Catalog=quanlykho;Integrated Security=True";
        protected SqlConnection conn = new SqlConnection(connStr);
        public static SqlConnection GetConn()
        {
            return new SqlConnection(connStr);
        }
    }
}
