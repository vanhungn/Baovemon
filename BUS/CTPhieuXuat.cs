using DTO;
using DTL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class CTPhieuXuat
    {
        DTL.CTPhieuXuat dal = new DTL.CTPhieuXuat();

        public string Update(ChiTietPhieuXuat ct)
        {
            try
            {
                if (ct.MaPX <= 0)
                    return "Chưa chọn phiếu nhập!";

                if (ct.MaMh <= 0)
                    return "Chưa chọn mã mặt hàng!";

                if (ct.SlXuat <= 0)
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
