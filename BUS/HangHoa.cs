using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO;
using DTL;

namespace BUS
{
    internal class HangHoa
    {
        HangHoaDTL dal = new HangHoaDTL();

        // 1. Lấy dữ liệu hiển thị
        public DataTable LayDanhSachSanPham()
        {
            return dal.GetDanhSachSanPham();
        }

        public DataTable LayDanhSachNCC()
        {
            return dal.GetDanhSachNCC();
        }

        public DataTable LayDanhSachLoaiHang()
        {
            return dal.GetDanhSachLoaiHang();
        }

        // 2. Chức năng Thêm
        public string Them(HangHoaDTO hh)
        {
            // Xử lý nghiệp vụ: Không cho phép tên mặt hàng bị trống
            if (string.IsNullOrWhiteSpace(hh.TenMh))
                return "Tên mặt hàng không được để trống!";

            try
            {
                // Gọi xuống DTL để thêm vào SQL
                if (dal.ThemSanPham(hh))
                    return "Thêm mới thành công!";
                return "Thêm thất bại!";
            }
            catch (Exception ex)
            {
                return "Lỗi CSDL: " + ex.Message;
            }
        }

        // 3. Chức năng Sửa
        public string Sua(HangHoaDTO hh)
        {
            if (string.IsNullOrWhiteSpace(hh.TenMh))
                return "Tên mặt hàng không được để trống!";

            try
            {
                if (dal.SuaSanPham(hh))
                    return "Cập nhật thành công!";
                return "Cập nhật thất bại!";
            }
            catch (Exception ex)
            {
                return "Lỗi CSDL: " + ex.Message;
            }
        }

        // 4. Chức năng Xóa
        public string Xoa(int maMh)
        {
            try
            {
                dal.XoaSanPham(maMh);
                return "Xóa thành công!";
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                // Mã 547 là lỗi vi phạm khóa ngoại (đang tồn tại trong bảng Tồn Kho)
                if (ex.Number == 547)
                    return "Sản phẩm này đang có trong kho, không thể xóa!";
                return "Lỗi CSDL: " + ex.Message;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }
    }
}
