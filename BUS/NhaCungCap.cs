using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class NhaCungCap
    {
        DTL.NhaCungCap nhaCungCap = new DTL.NhaCungCap();
        public DataTable GetNhaCungCap(DTO.NhaCungCap ncc)
        {
            return nhaCungCap.GetNhaCungCap(ncc);
        }
        public bool ThemNhaCungCap(DTO.NhaCungCap ncc)
        {
            return nhaCungCap.ThemNhaCungCap(ncc);
        }
        public bool SuaNhaCungCap(DTO.NhaCungCap ncc)
        {
            return nhaCungCap.SuaNhaCungCap(ncc) ;
        }
        public bool XoaNhaCungCap(DTO.NhaCungCap ncc)
        {
            return nhaCungCap.XoaNhaCungCap(ncc);
        }
        public DataTable GetCountNhaCungCap()
        {
            return nhaCungCap.GetTotalNhaCungCap();
        }
    }
}
