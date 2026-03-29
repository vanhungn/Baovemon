using System;
using System.Data;
using DTL;
using DTO;

namespace BUS
{
    public class PhieuNhapBUS
    {
        PhieuNhapDAL dal = new PhieuNhapDAL();

        // ================= GET =================
        public DataTable GetAll()
        {
            return dal.GetAll();
        }

        // ================= INSERT =================
        public int InsertAndGetID(PhieuNhapDTO pn)
        {
            return dal.InsertAndGetID(pn);
        }


        // ================= UPDATE =================
        public string Update(PhieuNhapDTO pn)
        {
            try
            {
                if (pn.MaPN <= 0)
                    return "Chưa chọn phiếu nhập!";

                return dal.Update(pn) ? "Sửa thành công!" : "Sửa thất bại!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // ================= DELETE =================
        public string Delete(int maPN)
        {
            try
            {
                if (maPN <= 0)
                    return "Chưa chọn phiếu nhập!";

                return dal.Delete(maPN) ? "Xóa thành công!" : "Xóa thất bại!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // ================= SEARCH =================
        public DataTable Search(string maPN, string ngay)
        {
            return dal.Search(maPN, ngay);
        }
        public DataTable GetCTPN(int maPN)
        {
            return dal.GetCTPN(maPN);
        }
        public int GetLastMaPN()
        {
            return dal.GetLastMaPN();
        }

        // Sửa (int maPN, int soLuong) thành (ChiTietPhieuNhap ct)
        
        public string InsertCTPN(ChiTietPhieuNhap ct)
        {
            return dal.InsertCTPN(ct)
                ? "OK"
                : "FAIL";
        }

    }
}