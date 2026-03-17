using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Data.SqlTypes;
using System.Data.Common;
using System.Data;

namespace DTL
{
    public class NhanVien:KetNoi
    {
        
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();

                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
        public DataTable GetNhanVien()
            
        {
            conn.Open();
            string query = "select *from nhan_vien";
            SqlDataAdapter daNhanVien = new SqlDataAdapter(query,conn);
            DataTable dtNhanhvien = new DataTable();
            dtNhanhvien.Clear();
            daNhanVien.Fill(dtNhanhvien);

            return dtNhanhvien;
        }
        public bool DangNhap(DTO.NhanVien nv)
        {
            conn.Open();

            string hashedPassword = HashPassword(nv.MatKhau);

            string query = "select * from nhan_vien where TaiKhoan=@TaiKhoan and MatKhau=@MatKhau";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@TaiKhoan", nv.TaiKhoan);
            cmd.Parameters.AddWithValue("@MatKhau", hashedPassword);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                conn.Close();
                return true;
            }

            conn.Close();
            return false;
        }
        public bool ThemNhanVien(DTO.NhanVien nv)
        {
            conn.Open();
            string hashedPassword = HashPassword(nv.MatKhau);
            string query = string.Format("insert into nhan_vien values " +
                "({0},{1},{2},{3},{4},{5},{6},{7})",nv.TenNV,nv.GioiTinh,nv.NgaySinh,nv.DiaChi,
                nv.DienThoai,nv.TaiKhoan, hashedPassword, nv.VaiTro);
            SqlCommand cmd = new SqlCommand(query,conn);
            if (cmd.ExecuteNonQuery() > 0)
            {
                return true;
            }
            return false;
        }

    }
}
