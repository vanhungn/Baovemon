using DTL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class CTPhieuNhap
    {
        DTL.CTPhieuNhap dal = new DTL.CTPhieuNhap();

        public string Update(ChiTietPhieuNhap ct)
        {
            try
            {
                if (ct.MaPN <= 0)
                    return "Chưa chọn phiếu nhập!";

                if (ct.MaMh <= 0)
                    return "Chưa chọn mã mặt hàng!";

                if (ct.SlNhap <= 0)
                    return "Số lượng phải lớn hơn 0!";

                return dal.Update(ct) ? "Sửa chi tiết thành công!" : "Sửa chi tiết thất bại!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
