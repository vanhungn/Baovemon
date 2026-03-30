using System;
using System.Data;
using DTO; // Khai báo để dùng được đối tượng TonKho
using DTL; // Khai báo để gọi được các hàm thêm, sửa, xóa dưới tầng DTL

namespace BUS
{
    // Đổi tên class thành TonKhoBUS để không bị nhầm lẫn (Name Clash) với DTO.TonKho
    public class TonKhoBUS
    {
        // Khởi tạo đối tượng của tầng DTL
        // Lưu ý: Nếu bên project DTL bạn đặt tên class là gì (ví dụ: TonKhoDAL hay TonKhoDTL), hãy sửa lại chữ TonKhoDAL ở dưới cho khớp nhé!
        private DTL.TonKho dtl = new DTL.TonKho();

        // 1. Hàm lấy danh sách Tồn kho
        public DataTable GetDanhSachTonKho()
        {
            // Chỉ cần gọi hàm từ DTL truyền lên
            return dtl.GetDanhSachTonKho();
        }

        // 2. Hàm Thêm Tồn kho
        public bool ThemTonKho(DTO.TonKho tk) // Viết DTO.TonKho cho chắc ăn, khỏi sợ C# nhận nhầm
        {
            // TRỌNG TÂM CỦA TẦNG BUS: Kiểm tra nghiệp vụ
            if (tk.soluong < 0)
            {
                // Báo lỗi ngược lên Form nếu người dùng nhập số âm
                throw new Exception("Số lượng tồn kho không được phép nhỏ hơn 0!");
            }

            // Nếu dữ liệu hợp lệ, gọi DTL để lưu vào CSDL
            return dtl.ThemTonKho(tk);
        }

        // 3. Hàm Sửa Tồn kho
        public bool SuaTonKho(DTO.TonKho tk)
        {
            if (tk.soluong < 0)
            {
                throw new Exception("Số lượng tồn kho không được phép nhỏ hơn 0!");
            }

            return dtl.SuaTonKho(tk);
        }

        // 4. Hàm Xóa Tồn kho
        public bool XoaTonKho(int maKho, int maMh)
        {
            // Xóa thì không cần kiểm tra số lượng, chỉ cần ném mã xuống DTL
            return dtl.XoaTonKho(maKho, maMh);
        }
    }
}