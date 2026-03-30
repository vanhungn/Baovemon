using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DTL
{
    public class CTPhieuNhap
    {
        string chuoiKN = @"Data Source=MEDIA\SQLEXPRESS;Initial Catalog=quanlykho;Integrated Security=True";
        public bool Update(ChiTietPhieuNhap ct)
        {
            string sql = @"UPDATE CT_Phieu_Nhap
                   SET SlNhap = @SlNhap
                   WHERE MaPN = @MaPN AND MaMh = @MaMh";

            using (SqlConnection conn = new SqlConnection(chuoiKN))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPN", ct.MaPN);
                    cmd.Parameters.AddWithValue("@MaMh", ct.MaMh);
                    cmd.Parameters.AddWithValue("@SlNhap", ct.SlNhap);

                    conn.Open();
                    int kq = cmd.ExecuteNonQuery();
                    return kq > 0;
                }
            }
        }
    }
}


