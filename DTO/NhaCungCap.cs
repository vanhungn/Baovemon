using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    internal class NhaCungCap
    {
		private int _MaNCC;

		public int MaNCC
        {
			get { return _MaNCC; }
			set { _MaNCC = value; }
		}
		private int _TenNCC;

		public int TenNCC
        {
			get { return _TenNCC; }
			set { _TenNCC = value; }
		}
		private int _DiaChi;

		public int DiaChi
        {
			get { return _DiaChi; }
			set { _DiaChi = value; }
		}
		private int _DienThoaiCC;

		public int MyProperty
		{
			get { return _DienThoaiCC; }
			set { _DienThoaiCC = value; }
		}



	}
}
