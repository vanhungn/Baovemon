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
       public DataTable GetDataNhanVien()
        {
            return nhanvien.GetNhanVien();

        }
        public bool ThemNhanVien(DTO.NhanVien tv)
        {
            return nhanvien.ThemNhanVien(tv);
        }

    }
}
