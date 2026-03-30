using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DTL
{
    public class TonKho : KetNoi
    {
        private string connectionString = @"Data Source=DESKTOP-3INS5UR\MSSQLSERVER01;Initial Catalog=quanlykho;Integrated Security=True";

        // 1. Hàm Lấy dữ liệu (SELECT)
        public DataTable GetDanhSachTonKho()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Dùng Alias [Mã Kho], [Mã Mặt Hàng]... để DataGridView hiển thị Tiếng Việt cho đẹp giống code cũ của bạn
                string query = "SELECT MaKho AS [Mã Kho], MaMh AS [Mã Mặt Hàng], SoLuong AS [Số Lượng] FROM ton_kho";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 2. Hàm Thêm (INSERT)
        public bool ThemTonKho(DTO.TonKho tk)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO ton_kho (MaKho, MaMh, SoLuong) VALUES (@MaKho, @MaMh, @SoLuong)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKho", tk.MaKho);
                    cmd.Parameters.AddWithValue("@MaMh", tk.MaMh);

                    // Chú ý: Gọi đúng tk.soluong (chữ s viết thường) theo như DTO của bạn
                    cmd.Parameters.AddWithValue("@SoLuong", tk.soluong);

                    // ExecuteNonQuery trả về số dòng bị ảnh hưởng. Nếu > 0 tức là thêm thành công.
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // 3. Hàm Sửa (UPDATE)
        public bool SuaTonKho(DTO.TonKho tk)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Sửa số lượng dựa vào Mã Kho và Mã Mặt Hàng
                string query = "UPDATE ton_kho SET SoLuong = @SoLuong WHERE MaKho = @MaKho AND MaMh = @MaMh";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKho", tk.MaKho);
                    cmd.Parameters.AddWithValue("@MaMh", tk.MaMh);
                    cmd.Parameters.AddWithValue("@SoLuong", tk.soluong); // tk.soluong chữ thường

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // 4. Hàm Xóa (DELETE)
        public bool XoaTonKho(int maKho, int maMh)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM ton_kho WHERE MaKho = @MaKho AND MaMh = @MaMh";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Hàm xóa không cần truyền cả cục DTO, chỉ cần truyền 2 khóa chính vào là đủ
                    cmd.Parameters.AddWithValue("@MaKho", maKho);
                    cmd.Parameters.AddWithValue("@MaMh", maMh);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
