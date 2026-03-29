using System;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DTL
{
    public class PhieuNhapDAL
    {
        SqlConnection conn = new SqlConnection(
            @"Data Source=MEDIA\SQLEXPRESS;Initial Catalog=quanlykho;Integrated Security=True");

        // ================= GET ALL (JOIN HIỂN THỊ TÊN) =================
        public DataTable GetAll()
        {
            string sql = @"
        SELECT 
            pn.MaPN, 
            pn.NgayNhap, 
            nv.TenNV, 
            kh.TenKho, 
            pn.Manv, 
            pn.MaKho,
            ISNULL(SUM(ct.SlNhap), 0) AS SlNhap -- Tính tổng số lượng, nếu chưa có thì để là 0
        FROM phieu_nhap pn
        JOIN nhan_vien nv ON pn.Manv = nv.Manv
        JOIN kho_hang kh ON pn.MaKho = kh.MaKho
        LEFT JOIN CT_phieu_nhap ct ON pn.MaPN = ct.MaPN -- Kết nối bảng chi tiết
        GROUP BY pn.MaPN, pn.NgayNhap, nv.TenNV, kh.TenKho, pn.Manv, pn.MaKho";

            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // ================= INSERT =================
        public int InsertAndGetID(PhieuNhapDTO pn)
        {
            try
            {
                conn.Open();

                string sql = @"
        INSERT INTO phieu_nhap(Manv, MaKho, NgayNhap)
        VALUES(@Manv, @MaKho, @NgayNhap);
        SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Manv", pn.Manv);
                cmd.Parameters.AddWithValue("@MaKho", pn.MaKho);
                cmd.Parameters.AddWithValue("@NgayNhap", pn.NgayNhap);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            finally
            {
                conn.Close();
            }
        }

        public bool InsertCTPN(ChiTietPhieuNhap ct)
        {
            string sql = @"INSERT INTO CT_phieu_nhap(MaPN, MaMh, SlNhap)
                   VALUES(@MaPN, @MaMh, @SlNhap)";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaPN", ct.MaPN);
            cmd.Parameters.AddWithValue("@MaMh", ct.MaMh);
            cmd.Parameters.AddWithValue("@SlNhap", ct.SlNhap);

            conn.Open();
            bool kq = cmd.ExecuteNonQuery() > 0;
            conn.Close();

            return kq;
        }
        // ================= UPDATE =================
        public bool Update(PhieuNhapDTO pn)
        {
            try
            {
                conn.Open();

                string sql = @"UPDATE phieu_nhap
                               SET Manv=@Manv, MaKho=@MaKho, NgayNhap=@NgayNhap
                               WHERE MaPN=@MaPN";
                string sqlCT = "UPDATE CT_phieu_nhap SET SlNhap = @SlNhap WHERE MaPN = @MaPN AND MaMh = @MaMh";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@MaPN", SqlDbType.Int).Value = pn.MaPN;
                cmd.Parameters.Add("@Manv", SqlDbType.Int).Value = pn.Manv;
                cmd.Parameters.Add("@MaKho", SqlDbType.Int).Value = pn.MaKho;
                cmd.Parameters.Add("@NgayNhap", SqlDbType.Date).Value = pn.NgayNhap;

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
        public bool Delete(int maPN)
        {
            try
            {
                conn.Open();

                string sql = "DELETE FROM phieu_nhap WHERE MaPN=@MaPN";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaPN", maPN);

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
    SELECT pn.MaPN, pn.NgayNhap,
           nv.TenNV, kh.TenKho,
           pn.Manv, pn.MaKho
    FROM phieu_nhap pn
    JOIN nhan_vien nv ON pn.Manv = nv.Manv
    JOIN kho_hang kh ON pn.MaKho = kh.MaKho
    WHERE (pn.MaPN LIKE @ma OR @ma = '')
    AND (CONVERT(date, pn.NgayNhap) = @ngay OR @ngay = '')";

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
        public DataTable GetCTPN(int MaPN)
        {
            string sql = @"SELECT ct.MaPN,ct.MaMh, hh.TenMh, ct.SlNhap,hh.DonGia, (ct.SlNhap * hh.DonGia) AS ThanhTien
            FROM CT_phieu_nhap ct
            JOIN hang_hoa hh ON ct.MaMh = hh.MaMh
            WHERE ct.MaPN = @MaPN";

            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.SelectCommand.Parameters.AddWithValue("@MaPN", MaPN);

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public int GetLastMaPN()
        {
            SqlConnection conn = new SqlConnection(
                @"Data Source=MEDIA\SQLEXPRESS;Initial Catalog=quanlykho;Integrated Security=True");

            SqlCommand cmd = new SqlCommand("SELECT MAX(MaPN) FROM phieu_nhap", conn);

            conn.Open();
            int ma = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();

            return ma;
        }
        
    }
}