using DTL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class PhieuXuatBUS
    {
        PhieuXuatDAL dal = new PhieuXuatDAL();

        // ================= GET =================
        public DataTable GetAll()
        {
            return dal.GetAll();
        }

        // ================= INSERT =================
        public int InsertAndGetid(PhieuXuatDTO pn)
        {
            return dal.InsertAndGetid(pn);
        }


        // ================= UPDATE =================
        public string Update(PhieuXuatDTO pn)
        {
            try
            {
                if (pn.MaPX <= 0)
                    return "Chưa chọn phiếu nhập!";

                return dal.Update(pn) ? "Sửa thành công!" : "Sửa thất bại!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // ================= DELETE =================
        public string Delete(int maPX)
        {
            try
            {
                if (maPX <= 0)
                    return "Chưa chọn phiếu nhập!";

                return dal.Delete(maPX) ? "Xóa thành công!" : "Xóa thất bại!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // ================= SEARCH =================
        public DataTable Search(string maPX, string ngay)
        {
            return dal.Search(maPX, ngay);
        }
        public DataTable GetCTPX(int maPX)
        {
            return dal.GetCTPX(maPX);
        }
        public int GetLastMaPX()
        {
            return dal.GetLastMaPX();
        }

        // Sửa (int maPN, int soLuong) thành (ChiTietPhieuXuat ct)

        public string InsertCTPX(ChiTietPhieuXuat ct)
        {
            return dal.InsertCTPX(ct)
                ? "OK"
                : "FAIL";
        }
    }
}

