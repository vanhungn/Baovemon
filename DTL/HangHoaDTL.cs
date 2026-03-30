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
    public class HangHoaDTL : KetNoi
    {
        

        public DataTable GetDanhSachSanPham()
        {
            conn.Open();
                string sql = @"SELECT h.MaMh, h.TenMh, h.LoaiMh, n.TenNCC 
                               FROM hang_hoa h 
                               JOIN nha_cung_cap n ON h.MaNCC = n.MaNCC";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            
        }
        public DataTable GetDanhSachNCC()
        {
           
                string sql = "SELECT MaNCC, TenNCC FROM nha_cung_cap ORDER BY TenNCC ASC";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            
        }

        // Lấy danh sách Loại hàng hóa cho ComboBox
        public DataTable GetDanhSachLoaiHang()
        {
      
                string sql = "SELECT DISTINCT LoaiMh FROM hang_hoa WHERE LoaiMh IS NOT NULL";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
           
        }

        public bool ThemSanPham(HangHoaDTO hh)
        {
           
                conn.Open();
                string sql = "INSERT INTO hang_hoa (TenMh, LoaiMh, MaNCC) VALUES (@ten, @loai, @ncc)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ten", hh.TenMh);
                cmd.Parameters.AddWithValue("@loai", hh.LoaiMh);
                cmd.Parameters.AddWithValue("@ncc", hh.MaNCC);

                return cmd.ExecuteNonQuery() > 0;
          
        }

        public bool SuaSanPham(HangHoaDTO hh)
        {
           
                conn.Open();
                string sql = "UPDATE hang_hoa SET TenMh=@ten, LoaiMh=@loai, MaNCC=@ncc WHERE MaMh=@ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ma", hh.MaMh);
                cmd.Parameters.AddWithValue("@ten", hh.TenMh);
                cmd.Parameters.AddWithValue("@loai", hh.LoaiMh);
                cmd.Parameters.AddWithValue("@ncc", hh.MaNCC);

                return cmd.ExecuteNonQuery() > 0;
           
        }

        public void XoaSanPham(int maMh)
        {
           
                conn.Open();
                string sql = "DELETE FROM hang_hoa WHERE MaMh=@ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ma", maMh);
                cmd.ExecuteNonQuery();
           
        }
    }
}
