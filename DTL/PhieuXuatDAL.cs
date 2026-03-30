using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL
{
    public class PhieuXuatDAL:KetNoi
    {
       

        // ================= GET ALL (JOIN HIỂN THỊ TÊN) =================
        public DataTable GetAll()
        {
            string sql = @"
        SELECT 
            pn.MaPX, 
            pn.NgayXuat, 
            nv.TenNV, 
            kh.TenKho, 
            pn.Manv, 
            pn.MaKho,
            ISNULL(SUM(ct.SlXuat), 0) AS SlXuat -- Tính tổng số lượng, nếu chưa có thì để là 0
        FROM phieu_xuat pn
        JOIN nhan_vien nv ON pn.Manv = nv.Manv
        JOIN kho_hang kh ON pn.MaKho = kh.MaKho
        LEFT JOIN CT_phieu_xuat ct ON pn.MaPX = ct.MaPX -- Kết nối bảng chi tiết
        GROUP BY pn.MaPX, pn.NgayXuat, nv.TenNV, kh.TenKho, pn.Manv, pn.MaKho";

            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // ================= INSERT =================
        public int InsertAndGetid(PhieuXuatDTO px)
        {
            using (SqlConnection conn = new SqlConnection(
                @"Data Source = LAPTOP-NG3J2HSN\\KTEAM; Initial Catalog = quanlykho; Integrated Security=True"))
            {
                string sql = @"
            INSERT INTO phieu_xuat(Manv, MaKho, NgayXuat)
            VALUES(@Manv, @MaKho, @NgayXuat);
            SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Manv", px.Manv);
                cmd.Parameters.AddWithValue("@MaKho", px.MaKho);
                cmd.Parameters.AddWithValue("@NgayXuat", px.NgayXuat);

                conn.Open();

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return 0;
                }
            }
        }


        // ================= UPDATE =================
        public bool Update(PhieuXuatDTO pn)
        {
            try
            {
                conn.Open();

                string sql = @"UPDATE phieu_xuat
                               SET Manv=@Manv, MaKho=@MaKho, NgayXuat=@NgayXuat
                               WHERE MaPX=@MaPX";
               
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@MaPX", SqlDbType.Int).Value = pn.MaPX;
                cmd.Parameters.Add("@Manv", SqlDbType.Int).Value = pn.Manv;
                cmd.Parameters.Add("@MaKho", SqlDbType.Int).Value = pn.MaKho;
                cmd.Parameters.Add("@NgayXuat", SqlDbType.Date).Value = pn.NgayXuat;

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi DAL Update: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        // ================= DELETE =================
        public bool Delete(int maPX)
        {
            try
            {
                conn.Open();

                string sql = "DELETE FROM phieu_xuat WHERE MaPX=@MaPX";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaPX", maPX);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi DAL Delete: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        // ================= SEARCH THEO NGÀY =================
        public DataTable Search(string maPN, string ngay)
        {
            string sql = @"
    SELECT pn.MaPX, pn.NgayXuat,
           nv.TenNV, kh.TenKho,
           pn.Manv, pn.MaKho
    FROM phieu_xuat pn
    JOIN nhan_vien nv ON pn.Manv = nv.Manv
    JOIN kho_hang kh ON pn.MaKho = kh.MaKho
    WHERE (pn.MaPX LIKE @ma OR @ma = '')
    AND (CONVERT(date, pn.NgayXuat) = @ngay OR @ngay = '')";

            SqlDataAdapter da = new SqlDataAdapter(sql, conn);

            // nếu rỗng thì truyền ""
            da.SelectCommand.Parameters.AddWithValue("@ma", "%" + maPN + "%");

            if (string.IsNullOrEmpty(ngay))
                da.SelectCommand.Parameters.AddWithValue("@ngay", DBNull.Value);
            else
                da.SelectCommand.Parameters.AddWithValue("@ngay", ngay);

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public DataTable GetCTPX(int MaPX)
        {
            string sql = @"
    SELECT ct.MaPX, ct.MaMh, hh.TenMh, ct.SlXuat
    FROM CT_phieu_xuat ct
    JOIN hang_hoa hh ON ct.MaMh = hh.MaMh
    WHERE ct.MaPX = @MaPX";

            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.SelectCommand.Parameters.AddWithValue("@MaPX", MaPX);

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public int GetLastMaPX()
        {
            SqlConnection conn = new SqlConnection(
                @"Data Source=MEDIA\SQLEXPRESS;Initial Catalog=quanlykho;Integrated Security=True");

            SqlCommand cmd = new SqlCommand("SELECT MAX(MaPX) FROM phieu_xuat", conn);

            conn.Open();
            int ma = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();

            return ma;
        }
        public bool InsertCTPX(ChiTietPhieuXuat ct)
        {
            // Chuỗi kết nối (Thay 'Ten_Database' bằng tên database của bạn)
            string connectionString = @"Data Source=MEDIA\SQLEXPRESS;Initial Catalog=quanlykho;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO CT_phieu_xuat (MaPX, MaMh, SlXuat) VALUES (@MaPN, @MaMh, @SlXuat)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                // Gán giá trị từ đối tượng ct vào câu lệnh SQL
                cmd.Parameters.AddWithValue("@MaPN", ct.MaPX);
                cmd.Parameters.AddWithValue("@MaMh", ct.MaMh);
                cmd.Parameters.AddWithValue("@SlNhap", ct.SlXuat);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
    }
}
