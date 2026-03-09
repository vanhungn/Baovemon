using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    internal class NhanVien
    {
		private int _Manv;
        private string _DienThoai;
		private string _TenNV;
		private string _GioiTinh;
		private string _NgaySinh;
		private string _DiaChi;

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
