using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL
{
    public class CTPhieuXuat : KetNoi
    {
        string chuoiKN = @"Data Source=MEDIA\SQLEXPRESS;Initial Catalog=quanlykho;Integrated Security=True";
        public bool Update(ChiTietPhieuXuat ct)
        {
            string sql = @"UPDATE CT_Phieu_Xuat
                   SET SlXuat = @SlXuat
                   WHERE MaPX = @MaPX AND MaMh = @MaMh";

            using (SqlConnection conn = new SqlConnection(chuoiKN))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPX", ct.MaPX);
                    cmd.Parameters.AddWithValue("@MaMh", ct.MaMh);
                    cmd.Parameters.AddWithValue("@SlXuat", ct.SlXuat);

                    conn.Open();
                    int kq = cmd.ExecuteNonQuery();
                    return kq > 0;
                }
            }
        }
    }
}
