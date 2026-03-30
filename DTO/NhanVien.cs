using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NhanVien
    {
		private int _Manv;
        private string _DienThoai;
		private string _TenNV;
		private string _GioiTinh;
		private string _NgaySinh;
		private string _DiaChi;
		private string _TaiKhoan;
        private string _MatKhau;
		private string _VaiTro;
		private int _skip;

		private string _newPassword;

		public string newPassword
        {
			get { return _newPassword; }
			set { _newPassword = value; }
		}

		public int skip
		{
			get { return _skip; }
			set { _skip = value; }
		}
		private int _limit;

		public int limit
		{
			get { return _limit; }
			set { _limit = value; }
		}

		public string VaiTro
        {
			get { return _VaiTro; }
			set { _VaiTro = value; }
		}

		public string TaiKhoan
        {
			get { return _TaiKhoan; }
			set { _TaiKhoan = value; }
		}
		

		public string MatKhau
        {
			get { return _MatKhau; }
			set { _MatKhau = value; }
		}


		public int Manv
        {
			get { return _Manv; }
			set { _Manv = value; }
		}
		public string TenNV
        {
			get { return _TenNV; }
			set { _TenNV = value; }
		}
		public string GioiTinh
        {
			get { return _GioiTinh; }
			set { _GioiTinh = value; }
		}
		public string NgaySinh
        {
			get { return _NgaySinh; }
			set { _NgaySinh = value; }
		}
		public string DiaChi
        {
			get { return _DiaChi; }
			set { _DiaChi = value; }
		}
		public string DienThoai
        {
			get { return _DienThoai; }
			set { _DienThoai = value; }
		}
	}
}
