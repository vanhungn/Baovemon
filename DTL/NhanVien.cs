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
        public DataTable GetNhanVien(DTO.NhanVien nv)
            
        {
            
            DataTable dtNhanhvien = new DataTable();
            try
            {
                conn.Open();
                string query = "";
                if (nv.TenNV == "" && nv.Manv==0)
                {
                    Console.WriteLine("dff");
                    query = "select * from nhan_vien order by Manv offset " + nv.skip +" rows fetch next " + nv.limit + " rows only";
                }
                else if (nv.TenNV != "" && nv.Manv == 0)
                {
                    query = "select *from nhan_vien where TenNV like '%" + nv.TenNV + "%'";

                }
                else if (nv.Manv > 0 && nv.TenNV == "")
                {
                    Console.WriteLine(nv.Manv);
                    query = "select *from nhan_vien where Manv= '" + nv.Manv + "'";
                }
                else
                {
                    query = "select *from nhan_vien where Manv ='" + nv.Manv + "' and TenNV like'%" + nv.TenNV + "%'";
                }
                SqlDataAdapter daNhanVien = new SqlDataAdapter(query, conn);
                dtNhanhvien.Clear();
                daNhanVien.Fill(dtNhanhvien);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
          
            return dtNhanhvien;

          
        }
        public string DangNhap(DTO.NhanVien nv)
        {
            conn.Open();

            string hashedPassword = HashPassword(nv.MatKhau);

            string query = "select * from nhan_vien where TaiKhoan=@TaiKhoan and MatKhau=@MatKhau";
            try
            {
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@TaiKhoan", nv.TaiKhoan);
                cmd.Parameters.AddWithValue("@MatKhau", hashedPassword);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string role = reader["VaiTro"].ToString();
                    return role;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return null;
        }
        public bool DoiMatKhau(DTO.NhanVien nv)
        {
            conn.Open();
            try
            {
                string hashedPasswordOld = HashPassword(nv.MatKhau);
                string hashedPasswordNew = HashPassword(nv.newPassword);

                string query = "UPDATE nhan_vien SET MatKhau = @MatKhauMoi WHERE TaiKhoan=@TaiKhoan AND  MatKhau = @MatKhauCu";
                SqlCommand cmd = new SqlCommand (query, conn);
                cmd.Parameters.AddWithValue("@TaiKhoan", nv.TaiKhoan);
                cmd.Parameters.AddWithValue("@MatKhauMoi", hashedPasswordNew);
                cmd.Parameters.AddWithValue("@MatKhauCu", hashedPasswordOld);
                if (cmd.ExecuteNonQuery()>0) {
                    return true;
                }
            }
            catch {
                throw;
                   }
            finally { conn.Close(); }
            return false;
        }
        public bool ThemNhanVien(DTO.NhanVien nv)
        {
            conn.Open();
            string hashedPassword = HashPassword(nv.MatKhau);
            string query = "INSERT INTO nhan_vien " +
                 "(TenNV, GioiTinh, NgaySinh, DiaChi, DienThoai, TaiKhoan, MatKhau, VaiTro) " +
                 "VALUES (@TenNV, @GioiTinh, @NgaySinh, @DiaChi, @DienThoai, @TaiKhoan, @MatKhau, @VaiTro)";
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenNV", nv.TenNV);
                    cmd.Parameters.AddWithValue("@GioiTinh", nv.GioiTinh);
                    cmd.Parameters.AddWithValue("@NgaySinh", nv.NgaySinh);
                    cmd.Parameters.AddWithValue("@DiaChi", nv.DiaChi);
                    cmd.Parameters.AddWithValue("@DienThoai", nv.DienThoai);
                    cmd.Parameters.AddWithValue("@TaiKhoan", nv.TaiKhoan);
                    cmd.Parameters.AddWithValue("@MatKhau", hashedPassword);
                    cmd.Parameters.AddWithValue("@VaiTro", nv.VaiTro);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                   
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally { conn.Close(); }

            return false;

        }
        public bool XoaNhanVien(DTO.NhanVien nv)
        {
            try
            {
                conn.Open();
                string query = "delete nhan_vien where Manv=@Manv";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Manv", nv.Manv);
                if(cmd.ExecuteNonQuery() > 0) { return true; }
            }
            catch (Exception ) { throw ; }
           
            finally
            {
                conn.Close();
            }
            return false;
        }

        public bool SuaNhanVien(DTO.NhanVien nv)
        {
            try
            {
                conn.Open();

                string query;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (!string.IsNullOrEmpty(nv.MatKhau))
                {
                    string hashedPassword = HashPassword(nv.MatKhau);

                    query = "UPDATE nhan_vien SET TenNV=@TenNV, GioiTinh=@GioiTinh, NgaySinh=@NgaySinh, " +
                            "DiaChi=@DiaChi, DienThoai=@DienThoai, TaiKhoan=@TaiKhoan, MatKhau=@MatKhau, VaiTro=@VaiTro " +
                            "WHERE Manv=@Manv";

                    cmd.Parameters.AddWithValue("@MatKhau", hashedPassword);
                }
                else
                {
                    query = "UPDATE nhan_vien SET TenNV=@TenNV, GioiTinh=@GioiTinh, NgaySinh=@NgaySinh, " +
                            "DiaChi=@DiaChi, DienThoai=@DienThoai, TaiKhoan=@TaiKhoan, VaiTro=@VaiTro " +
                            "WHERE Manv=@Manv";
                }

                cmd.CommandText = query;

                cmd.Parameters.AddWithValue("@TenNV", nv.TenNV);
                cmd.Parameters.AddWithValue("@GioiTinh", nv.GioiTinh);
                cmd.Parameters.AddWithValue("@NgaySinh", nv.NgaySinh);
                cmd.Parameters.AddWithValue("@DiaChi", nv.DiaChi);
                cmd.Parameters.AddWithValue("@DienThoai", nv.DienThoai);
                cmd.Parameters.AddWithValue("@TaiKhoan", nv.TaiKhoan);
                cmd.Parameters.AddWithValue("@VaiTro", nv.VaiTro);
                cmd.Parameters.AddWithValue("@Manv", nv.Manv);
                Console.WriteLine(nv.Manv);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close(); 
            }
            return false;
        }
        public DataTable GetTotalNhanvien()
        {
            DataTable dtNhanVien = new DataTable();
            try {
                conn.Open();
                string query = "select * from nhan_vien";
                SqlDataAdapter daNhanVien= new SqlDataAdapter(query,conn);
                dtNhanVien.Clear();
                daNhanVien.Fill(dtNhanVien);

            }
            catch {
                throw;
            }
            finally { conn.Close(); }
            return dtNhanVien;
        }
    }
}
