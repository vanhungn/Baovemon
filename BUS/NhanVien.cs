using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class NhanVien
    {
        DTL.NhanVien nhanvien = new DTL.NhanVien();
       public DataTable GetDataNhanVien(DTO.NhanVien nv)
        {
            return nhanvien.GetNhanVien(nv);

        }
        public bool ThemNhanVien(DTO.NhanVien tv)
        {
            return nhanvien.ThemNhanVien(tv);
        }
        public bool SuaNhanVien(DTO.NhanVien nv)
        {
            return nhanvien.SuaNhanVien(nv);
        }
        public bool XoaNhanvien(DTO.NhanVien nv)
        {
            return nhanvien.XoaNhanVien(nv);
        }
        public DataTable GetCountNhanVien()
        {
            return nhanvien.GetTotalNhanvien();
        }
        public string DangNhap(DTO.NhanVien nv)
        {
            return nhanvien.DangNhap(nv);
        }
        public bool DoiMatKhau(DTO.NhanVien nv)
        {
            return nhanvien.DoiMatKhau(nv);
        }
    }
}
