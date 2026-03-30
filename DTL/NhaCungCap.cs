using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.SymbolStore;

namespace DTL
{
    public class NhaCungCap : KetNoi
    {
        public DataTable GetNhaCungCap(DTO.NhaCungCap ncc)

        {

            DataTable dtNhaCC = new DataTable();
            try
            {
                conn.Open();
                string query = "";
                if (ncc.TenNCC == "" && ncc.MaNCC == 0)
                {
                    query = "select * from nha_cung_cap order by MaNCC offset " + ncc.skip + " rows fetch next " + ncc.limit + " rows only";
                }
                else if (ncc.TenNCC != "" && ncc.MaNCC == 0)
                {
                    query = "select *from nha_cung_cap where MaNCC like '%" + ncc.TenNCC + "%'";

                }
                else if (ncc.MaNCC > 0 && ncc.TenNCC == "")
                {
                    query = "select *from nha_cung_cap where MaNCC= '" + ncc.MaNCC + "'";
                }
                else
                {
                    query = "select *from nha_cung_cap where MaNCC ='" + ncc.MaNCC + "' and TenNCC like'%" + ncc.TenNCC + "%'";
                }
                SqlDataAdapter daNhanVien = new SqlDataAdapter(query, conn);
                dtNhaCC.Clear();
                daNhanVien.Fill(dtNhaCC);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return dtNhaCC;

        }
        public bool ThemNhaCungCap(DTO.NhaCungCap ncc)
        {
            conn.Open();
            string query = "INSERT INTO nha_cung_cap " +
                 "(TenNCC, DiaChi, DienThoaiCC, Email) " +
                 "VALUES (@TenNCC, @DiaChi, @DienThoaiCC, @Email)";
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenNCC", ncc.TenNCC);
                    cmd.Parameters.AddWithValue("@DiaChi", ncc.DiaChi);
                    cmd.Parameters.AddWithValue("@DienThoaiCC", ncc.DienThoaiCC);
                    cmd.Parameters.AddWithValue("@Email", ncc.Email);
                    ;
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { conn.Close(); }

            return false;

        }
        public bool SuaNhaCungCap(DTO.NhaCungCap ncc)
        {
            try
            {
                conn.Open();
                string query = "UPDATE nha_cung_cap SET TenNCC=@TenNCC, DiaChi=@DiaChi, DienThoaiCC=@DienThoaiCC, " +
                            "Email=@Email" +
                           " WHERE MaNCC=@MaNCC";
                SqlCommand cmd = new SqlCommand(query,conn);
               
                   
                cmd.Parameters.AddWithValue("@TenNCC", ncc.TenNCC);
                cmd.Parameters.AddWithValue("@DiaChi", ncc.DiaChi);
                cmd.Parameters.AddWithValue("@DienThoaiCC", ncc.DienThoaiCC);
                cmd.Parameters.AddWithValue("@Email", ncc.Email);
                cmd.Parameters.AddWithValue("@MaNCC", ncc.MaNCC);
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
        public bool XoaNhaCungCap(DTO.NhaCungCap ncc)
        {
            conn.Open();
            try
            {
                string query = "delete nha_cung_cap where MaNCC=@MaNCC ";
                SqlCommand cmd = new SqlCommand(query,conn);
                cmd.Parameters.AddWithValue("@MaNCC", ncc.MaNCC);
                if(cmd.ExecuteNonQuery() > 0) { return true; }
            }
            catch
            {
                throw;
            }
            finally { conn.Close(); }
            return false;
        }
        public DataTable GetTotalNhaCungCap()
        {
            DataTable dtNhaCungCap= new DataTable();
            try
            {
                conn.Open();
                string query = "select * from nha_cung_cap";
                SqlDataAdapter daNhanVien = new SqlDataAdapter(query, conn);
                dtNhaCungCap.Clear();
                daNhanVien.Fill(dtNhaCungCap);

            }
            catch
            {
                throw;
            }
            finally { conn.Close(); }
            return dtNhaCungCap;
        }
    }
}
