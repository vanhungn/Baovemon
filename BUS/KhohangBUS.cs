using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class KhohangBUS
    {
        string conn = @"Data Source=MEDIA\SQLEXPRESS;Initial Catalog=quanlykho;Integrated Security=True";

        public DataTable GetAll()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MaKho, TenKho FROM kho_hang", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
